using System;
using System.Collections.Generic;
using System.Linq;
using Nest;
using System.Web;
using Elasticsearch.Net;
using CRCIS.Web.INoor.CRM.Contract.ElkSearch;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Domain.Users.UserSearch;

namespace CRCIS.Web.INoor.CRM.Infrastructure.ElkSearch
{
    public class UserSearch : IUserSearch
    {
        private static ElasticClient _singletonClient;
        private static readonly object locker = new object();
        private static ElasticClient getSingletonClient()
        {
            var obj = new Object();
            if (_singletonClient == null)
            {
                lock (locker)
                {
                    if (_singletonClient == null)
                    {
                        var connectionSettings = new ConnectionSettings(new Uri("http://search.elk.pars.local:9200/"))
                            .BasicAuthentication("NoormagsUserSearch", "NoormagsUserSearch")
                            .DisableDirectStreaming()
                            .DefaultMappingFor<PersonSearchViewModel>(a => a.IndexName("accounts-person").IdProperty(d => d.Id));
                        _singletonClient = new ElasticClient(connectionSettings);
                    }
                }
            }

            return _singletonClient;
        }

        public IEnumerable<PersonSearchViewModel> Search(string fullName, string mobile, string username, string email)
        {
            var searchRequest = buildQuery(fullName, mobile, username, email);
            var json = getSingletonClient().RequestResponseSerializer.SerializeToString(searchRequest);
            var searchResponse = getSingletonClient().Search<PersonSearchViewModel>(searchRequest);

            var people = searchResponse.Documents;
            return people.ToList();
        }

        public async Task<SearchUserOutputDto> SearchAsync(string fullName, string mobile, string username, string email)
        {
            var searchRequest = buildQuery(fullName, mobile, username, email);
            var json = getSingletonClient().RequestResponseSerializer.SerializeToString(searchRequest);
            var searchResponse = await getSingletonClient().SearchAsync<PersonSearchViewModel>(searchRequest);
            var listElk = searchResponse.Documents.ToList();

            var listFinal = new List<SeadrchUserDto>();
            var listTemp = listElk.Select(a => new { UserId = a.Id, Fullname = a.FullName, }).Distinct().ToList();

            foreach (var item in listTemp)
            {
                var identities = listElk.Where(a => a.Id == item.UserId).SelectMany(i => i.Identities).ToList();
                listFinal.Add(new SeadrchUserDto
                {
                    UserId = item.UserId.ToString(),
                    Fullname = item.Fullname,
                    Username = string.Join(" , ", identities.Where(a => a.IdentityTypeId == 3).Select(a => a.IdentityValue).ToArray()),
                    Email = string.Join(" , ", identities.Where(a => a.IdentityTypeId == 2).Select(a => a.IdentityValue).ToArray()),
                    Mobile = string.Join(" , ", identities.Where(a => a.IdentityTypeId == 1).Select(a => a.IdentityValue).ToArray()),

                });

            }

            return new SearchUserOutputDto { IsSuccess = true, Data = listFinal, TotalCount = listFinal.Count() };
        }

        private SearchRequest<PersonSearchViewModel> buildQuery(string fullName, string mobile, string username, string email)
        {
            var mustQueries = new List<QueryContainer>();

            if (string.IsNullOrEmpty(fullName) == false)
            {
                var qFullName = addFullName(fullName);
                mustQueries.Add(qFullName);
            }

            if (string.IsNullOrEmpty(mobile) == false)
            {
                if (mobile.StartsWith("09"))
                {
                    var temp = mobile.Substring(1);
                    var withStarStarting = $"+98-{temp}";
                    var qStared = addIdentity(withStarStarting);
                    mustQueries.AddRange(qStared);
                }
                else
                {
                    var t = addIdentity(mobile);
                    mustQueries.AddRange(t);
                }
            }
            if (string.IsNullOrEmpty(email) == false)
            {
                var t = addIdentity(email);
                mustQueries.AddRange(t);
            }

            if (string.IsNullOrEmpty(username) == false)
            {
                var t = addIdentity(username);
                mustQueries.AddRange(t);
            }


            QueryContainer queryContainer = new BoolQuery
            {
                Must = mustQueries.ToArray()
            };
            var searchRequest = new SearchRequest<PersonSearchViewModel>
            {
                Query = queryContainer
            };
            return searchRequest;
        }

        private QueryContainer addFullName(string fullname)
        {
            if (fullname.Contains('*'))
            {
                var qWildcardQuery = new WildcardQuery { Field = "fullName", Value = fullname, Boost = 4 };
                return qWildcardQuery;
            }

            var qFullName = new BoolQuery
            {
                Should = new List<QueryContainer>
                {
                    new MatchPhraseQuery{Field ="fullName" ,Query = fullname , Boost = 4 },
                    new MatchQuery{Field="fullName" ,Query =fullname, Operator = Operator.And, Boost = 2},
                    new MatchQuery{Field="fullName" ,Query =fullname, Operator = Operator.Or, Boost = 1},
                }
            };

            return qFullName;
        }

        private static List<QueryContainer> addIdentity(string identity)
        {
            List<QueryContainer> mustQueries = new List<QueryContainer>();
            if (string.IsNullOrEmpty(identity) == false)
            {
                if (identity.Contains('*'))
                {
                    var q1 = new BoolQuery
                    {
                        Should = new List<QueryContainer>
                        {
                            new WildcardQuery { Field = "identities.identityValue", Value = identity, Boost = 4},
                            new MatchQuery{Field="identities.identityValue" ,Query = identity, Operator = Operator.And , Boost = 2 },
                            new MatchQuery{Field="identities.identityValue" ,Query = identity, Operator = Operator.Or  , Boost = 1 },
                        },

                    };

                    //var qWildcardQuery = new WildcardQuery { Field = "identities.identityValue", Value = identity, };
                    mustQueries.Add(q1);
                }
                else
                {
                    var q2 = new BoolQuery
                    {
                        Should = new List<QueryContainer>
                    {
                        new MatchPhraseQuery{Field ="identities.identityValue" ,Query = identity, Boost = 4},
                         new MatchQuery{Field="identities.identityValue" ,Query = identity, Operator = Operator.And , Boost = 2 },
                         new MatchQuery{Field="identities.identityValue" ,Query = identity, Operator = Operator.Or , Boost = 1 },
                    }
                    };
                    mustQueries.Add(q2);
                }
            }

            return mustQueries;

        }

    }

    #region Models
    [ElasticsearchType(RelationName = "accounts-person")]
    public class PersonSearchViewModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public bool? IsDeleted { get; set; }
        public long? FirstIP { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int AccessFailedCount { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public PersonSearchType Type { get; set; }
        public PersonSearchLanguage Language { get; set; }
        public PersonSearchClient FirstClient { get; set; }
        public PersonSearchReal Person { get; set; }
        public PersonSearchOrganization Organization { get; set; }
        public PersonSearchIdentity[] Identities { get; set; }
    }
    public class PersonSearchIdentity
    {
        public long Id { get; set; }
        public int IdentityTypeId { get; set; }
        public string IdentityTypeName { get; set; }
        public string IdentityValue { get; set; }
        public bool IsVerified { get; set; }
        public bool IsLoginable { get; set; }
    }

    public class PersonSearchOrganization
    {
        public string Name { get; set; }
        public string Agent { get; set; }
        public string AgentPosition { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public bool? IsImpersonate { get; set; }
        public Guid? ImpersonateUserID { get; set; }

    }

    public class PersonSearchReal
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Proficiency { get; set; }
        public bool Sex { get; set; }
        public int? GradeId { get; set; }
        public string GradeName { get; set; }
        public int? HawzahGradeId { get; set; }
        public string HawzahGradeName { get; set; }
    }

    public class PersonSearchClient
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Name { get; set; }
    }

    public class PersonSearchLanguage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class PersonSearchType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }
    #endregion
}
