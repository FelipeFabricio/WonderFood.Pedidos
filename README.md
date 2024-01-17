
# WonderFood 


## Sobre
Tech Challenge da Pós Graduação de Software Architecture da FIAP, com conclusão prevista 07/2024.
O projeto é a construção de um Sistema de autoatendimento de um fast food em expansão, com foco no Backend e infraestrutura na Cloud.

<br>

## Links e Documentação
- #### Diagrama Entidade Relacional: https://whimsical.com/diagrama-entidade-relacional-DkvDA7mHVfb2z8qYRKARtK
- #### Desenho de Arquitetura: Em breve
- #### Sumário Ubíquo: https://whimsical.com/sumario-da-linguagem-ubiqua-XbBVLsf1ZSMEF6iV2FWooi
- #### Repositório GitHub: https://github.com/FelipeFabricio/WonderFood
<br>

## Tecnologias utilizadas

1. .Net 7
3. SQL Server
4. Docker
5. Kubernetes
<br>

##  Procedimentos de execução

Via Docker-compose:

 1. Executar o comando: `docker-compose up -d` 
 2. Acessar a aplicação usando o **SwaggerUI** através do seu localhost, na porta 80. Exemplo: http://localhost:80/swagger
<br>

Via manifesto Kubernetes:
 1. Executar o comando: `kubectl apply -f deployment.yml` 
 2. Acessar a aplicação usando o **SwaggerUI** através do seu localhost, na porta 80. Exemplo: http://localhost:80/swagger
