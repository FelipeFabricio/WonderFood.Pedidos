
# WonderFood
![Wonderfood](https://camo.githubusercontent.com/851091d5086ade74a55dbdfba0ee7303c81f55c00ebfa6fd47981909ac0b4776/68747470733a2f2f66696c65732e6f616975736572636f6e74656e742e636f6d2f66696c652d6a68546f4137663845654848304f3855697657476f6659523f73653d323032342d30312d3139543138253341333225334134385a2673703d722673763d323032312d30382d30362673723d6226727363633d6d61782d6167652533443331353336303030253243253230696d6d757461626c6526727363643d6174746163686d656e7425334225323066696c656e616d6525334466613761623063652d656666372d346361352d386233612d6538663635663138636635302e77656270267369673d415a364b77784e417146784263614b387161685567576b657a4c7257445130685167744446544563754849253344)
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
    
  

