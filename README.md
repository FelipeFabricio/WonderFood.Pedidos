
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=FelipeFabricio_wonderfood-pedidos&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=FelipeFabricio_wonderfood-pedidos)
[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=FelipeFabricio_wonderfood-pedidos&metric=bugs)](https://sonarcloud.io/summary/new_code?id=FelipeFabricio_wonderfood-pedidos)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=FelipeFabricio_wonderfood-pedidos&metric=coverage)](https://sonarcloud.io/summary/new_code?id=FelipeFabricio_wonderfood-pedidos)

# WonderFood.Pedidos

## :information_source: Sobre

Sistema responsável por toda a parte de Pedidos do restaurante WonderFood, tendo como principais funcionalidades:

- Criação de Pedidos
- Consulta do Status atual de um Pedido
- Envia para Processamento de Pagamento
- Envio para Produção do Pedido
<br>

## :scroll: Links e Documentação


- #### Diagrama Entidade Relacional: https://shre.ink/DGGG
- #### Fluxo de Pedido e Pagamento: https://shre.ink/DGGi
- #### Fluxo da Aplicação: https://shre.ink/DGGj
- #### Sumário Ubíquo: https://shre.ink/DGGX
- #### Arquitetura de Infraestrutura no Azure: https://shre.ink/DGGC
- #### Fluxo Autenticação com Azure AD B2C: https://shre.ink/DGGv
- #### Relatório OWASP Inicial: https://shre.ink/DGha
- #### Relatório OWASP Final: https://shre.ink/DGhH
- #### Relatório RIPD: https://shre.ink/DGh9

<br>

## :hammer_and_wrench:  Tecnologias utilizadas

1. .Net 8
2. Entity Framework
3. MongoDb
4. Docker
5. Kubernetes / AKS
6. Terraform
7. CI/CD / Github Actions
8. BDD /Specflow
9. Xunit
10. SonarCloud
<br>

## :classical_building:  Padrões de Arquitetura e Design

1. Arquitetura em Camadas
4. Repository Pattern
5. SAGA Pattern com Masstransit

## Justificativa da escolha de SAGAS Orquestradas com Masstransit


**Porque Sagas Orquestradas:** Optei por trabalhar com esse tipo de Saga por centralizar a lógica de coordenação dentro de um único local, facilitando a manutenção, visibilidade e monitoramento das Sagas, além de reduzir bastante a complexidade.
Até pensei inicialmente em trabalhar com Sagas coreografadas, porém trabalhar com sistemas distribuídos e ter a lógica da Saga espalhada por esses serviços, deixa o desenvolvimento e o processo de 'debug' muito mais complexo, e ***não traz nenhuma vantagem relevante para o ecossistema das aplicação como está hoje**.*
<br>


**Porque Masstransit:** MassTransit é uma biblioteca para .NET que fornece uma plataforma moderna e focada no desenvolvedor para a criação de aplicações distribuídas sem complexidade. Entre os principais fatores que me fizeram optar por utilizá-lo foram:

 -   **Facilidade de Uso**: Simplifica e **MUITO** a configuração e o gerenciamento de fluxos de trabalho das SAGAS.
 -   **Integração Nativa**:  Integra-se facilmente com RabbitMQ, que foi a Message Broker escolhido por mim inicialmente.
 -   **Experiência Prévia**: O fato de já ter trabalhado com a biblioteca anterioremente e ter obtido ótimos resultados e experiência durante esse período, também pesaram para a escolha.
 -   **Documentação e Suporte**: Possui excelente documentação e suporte, inclusive com vídeos publicados pelo próprio criador da biblioteca.
 

Links úteis:
- **Documentação Masstransit:** https://masstransit.io/
- **Canal do Criado da Biblioteca:** https://www.youtube.com/@PhatBoyG


<br>
