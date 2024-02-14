
# WonderFood

## Sobre
Tech Challenge da Pós Graduação de Software Architecture da FIAP, com conclusão prevista 07/2024.
O projeto é a construção de um Sistema de autoatendimento de um fast food em expansão, com foco no Backend e infraestrutura na Cloud.
<br>

## :scroll: Links e Documentação
- #### Diagrama Entidade Relacional: https://encurtador.com.br/pvwNO
- #### Fluxo de Pedido e Pagamento: https://encurtador.com.br/puvP3
- #### Sumário Ubíquo: https://encurtador.com.br/bfVY8
- #### Repositório GitHub: https://github.com/FelipeFabricio/WonderFood
- ####  Desenho Arquitetura Infraestrutura: https://encurtador.com.br/nuNOT
<br>

## :hammer_and_wrench:  Tecnologias utilizadas

1. .Net 7
3. SQL Server
4. Docker
5. Kubernetes~~~~
6. AKS
<br>

##  :arrow_forward: Procedimentos de execução

**Via Docker-compose:**

 1. Na pasta raiz da solução, execute o comando: `docker-compose up -d` 
 2. Acessar a aplicação usando o **SwaggerUI** através do seu localhost, na porta 8000. Exemplo: http://localhost:8000/swagger
<br>

**Via Kubernetes Local:**
 1. Criar um Secret para a senha do SQL Server usando o comando:
 `kubectl create secret generic sqlserver-password-secret --from-literal=SA_PASSWORD='senhaTeste123!'`
 
 2. Criar um Secret para a string de conexão da aplicação com a base de dados:
 `kubectl create secret generic database-connection-secret --from-literal=connectionstring='Server=wonderfood-sqlserver-service,1433;Database=master;User Id=sa;Password=senhaTeste123!;TrustServerCertificate=True'`

3. Na pasta raiz da solução, execute o manifesto de Deployment com o comando: `kubectl apply -f deployment.yml`

4. Para facilitar o acesso, encaminhe uma porta para o pod com o comando:
`kubectl port-forward service/wonderfood-app-service 8080:80`

 5. Acessar a aplicação usando o **SwaggerUI** através do seu localhost, na porta 8080. Exemplo: http://localhost:8080/swagger
    
  

