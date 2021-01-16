# process-express
[![Build Status](https://www.travis-ci.com/thiagocoppi/process-express.svg?branch=main)](https://www.travis-ci.com/thiagocoppi/process-express)

Isso é uma API para processamento de arquivos OFX

Dentro da pasta base há um arquivo de configuração para subir os containers básicos da aplicação, esses dois comando irá subir as instâncias do PostgreSQL, Kibana, Elasticsearch.
```
docker network create elastic
docker-compose up -d
```

Neste projeto foram utilizados como centralizador de logs o elasticsearch juntamente com o kibana. O banco de dados postgresql foi escolhido pela familiaridade com o banco de dados.

Foi utilizado nesse projeto;

- CQRS (Mediatr) - Para contexto de negócios onde temos regras complexas o CQRS se aplica muito bem e deixa os comandos mais didáticos para a área de negócio
- DDD Domain driven Design - Para aproximar o código ao negócio 
- PostgreSQL como banco de dados
- Elasticsearch/Kibana - Para armazenagem de logs de aplicação/erros 
- Serilog - Para utilização dos logs
- Dapper como ORM principal, motivo - como o contexto de negócio possui muitas requisições foi escolhido para otimização das querys
- Fluent Migrator - Para controle da base de dados com as criações de campos etc...
- OfxSharp - Para leitura dos arquivos Ofx
- NSwag - Para documentação das APIs
- NSubstitute - Para fazer os mocks no contexto do teste, para não fazer vínculo com a base de dados
- Autenticação JWT - Para realizar a autenticação basta preencher o usuário/instituição que ele irá gerar um token para ser utilizados nos demais endpoints

Para esteira de build foi escolhido o TravisCI do qual consta no início do Readme o link dos builds e seu status.

Pontos que necessitariam de melhoria identificados nesta solução;
- Melhorar a parte de infraestrutura para utilizar os volumes do docker no dockerfile para armazenar os arquivos OFX dentro
- Criar alguma abstração de componentes para IoC

