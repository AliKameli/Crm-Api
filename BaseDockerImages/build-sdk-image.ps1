docker image build -t 172.16.25.82:443/crm/crm-sdk:6.0 .
docker push 172.16.25.82:443/crm/crm-sdk:6.0
docker image rm -f 172.16.25.82:443/crm/crm-sdk:6.0