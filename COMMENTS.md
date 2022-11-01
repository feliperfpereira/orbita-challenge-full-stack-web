# Orbita Challenge!

Projeto desenvolvido para realização de um teste para A+ Educação com intuito de validar e demonstrar conhecimento nas linguagens avaliadas.


# Decisão da Arquitetura Utilizada

Criado em Arquitetura MVC, largamente utilizado no mercado onde é criada uma camada limpa de model que facilmente é atrelado ao banco de dados, com a unica diferença que criei além das models padrão outras 3 classes para o mesmo objeto( studentRead, studentCreate, studentUpdate) onde consigo criar regras diferentes para cara possível retorno, como por exemplo no create não existe o id pois é um objeto a ser criado, e caso queira fazer validações apenas na criação ou atualização consigo inserir diferentes dataAnnotations.
Utilizei para criação do backend .net core 6.0 minimal API, nova tecnilogia criada a partir do .net core 6.0 onde é possivel criar um api restfull com menos de 50 linhas se aproximando bastante da codificação de uma API NODEJS utilizando expressJs.
Banco de dados foi MySQL largamente utilizado no mercado, o escolhi devido a minha familiaridade com a tecnologia.
Já no front end foi criado em VUEJS versão 3 utilizando o framework quasar, este ultimo foi uma escolha pessoal pois nunca havia desenvolvido em Vue porem olhando em alguns aspectos como compatibilidade, pois com ele você consegue desenvolver SPA, SSR, PWA, mobile(Capacitor/Cordova) e desktop(Electron), em analises de performance foi aferido que é mais rápido e também pela quantidades de componentes disponíveis, então pensando no longo prazo e multiplataformas escolhi o quasar.

## Lista de bibliotecas de terceiros utilizada.

.NET CORE 6.0
MySQL 5.7
Vue3
Quasar 2.10.1
Pomelo Mysql
xUnit 2.4.1

## Oque melhoraria se tivesse mais tempo

Junto com um designer, desenvolveria um mockup de baixa e alta fidelidade e após aprovações dos stackholders, redesenharia todo o front end baseado no protótipo.
Implementaria uma segurança utilizando JWT, onde é criado um chave no backend, e toda a comunicação entre front e back é passado para validar que o solicitante tem as permissões e autorizações para acessar os endpoints.
Implementação de uma arquitetura CQRS, onde há muito paralelismo, a quantidade de leituras for muito maior que a inserção de dados, diversas integrações onde se algo falhar o todo não pode parar a arquitetura CQRS seria recomendada.
Conteinerização ; hoje onde há diversos serviços clouds, um sistema dockerizado pode facilidade transitar entre os serviços disponíveis afim de buscar o com melhor custo beneficio.
Logs: Inserção do serilog para que em ambiente de produção caso ocorra erros no backend o mesmo possa ser auditavel.
Caso o sistema seja aberto ao publico geral, autenticação com Google OAuth 

## Como rodar o sistema

Backend:

    dotnet build
    dotnet run

frontend:

    npm i
    npm i -g @quasar/cli
    quasar dev

