# RegistrosCT-e API

## Descrição

Esta é uma API RESTful desenvolvida em ASP.NET Core para o gerenciamento de Conhecimentos de Transporte Eletrônico (CT-e) e informações relacionadas a viagens, cargas e despesas adicionais. A API permite criar, consultar, atualizar e excluir registros de CT-e.

## Funcionalidades Principais

*   **Gerenciamento de CT-e:**
    *   Criação de novos CT-e.
    *   Consulta de CT-e por ID.
    *   Listagem de todos os CT-e cadastrados.
    *   Atualização de dados de um CT-e existente.
    *   Exclusão de CT-e.
*   **Gerenciamento de Viagens:**
    *   Criação de novas viagens, associadas a uma carga.
    *   Consulta de viagens por ID.
    *   Listagem de todas as viagens.
    *   Atualização de dados de uma viagem existente.
    *   Exclusão de viagens.
*   **Gerenciamento de Cargas:**
    *   Criação de novas cargas, especificando quantidade, peso e volume.
    *   Consulta de cargas por ID.
    *   Listagem de todas as cargas.
    *   Atualização de dados de uma carga existente.
    *   Exclusão de cargas.
*   **Gerenciamento de Despesas Adicionais:**
    *   Criação de despesas adicionais associadas a uma viagem.
    *   Consulta de despesas adicionais por ID.
    *   Listagem de todas as despesas adicionais.
    *   Atualização de dados de uma despesa adicional existente.
    *   Exclusão de despesas adicionais.
*   **Cálculo de Frete:**
    *   Cálculo automático do valor do frete com base no peso da carga e nas despesas adicionais.
*   **Validações:**
    *   Validação robusta dos dados de entrada, garantindo a integridade dos registros.

## Fluxo de Uso e Casos de Uso

Este sistema segue um fluxo específico para garantir a integridade dos dados e a correta associação entre as entidades. Abaixo, descrevemos a ordem de criação e as restrições de atualização para cada entidade:

**1. Carga:**

*   **Criação:** A entidade `Carga` deve ser a primeira a ser criada. Ela representa os detalhes da carga a ser transportada (quantidade, peso, volume).
*   **Atualização:** Uma `Carga` só pode ser atualizada se não estiver vinculada a nenhuma `Viagem`. Isso garante que as informações da carga não sejam alteradas após o início de uma viagem.

**2. Viagem:**

*   **Criação:** Para criar uma `Viagem`, é necessário que já exista uma `Carga` previamente cadastrada. A `Viagem` representa o trajeto a ser percorrido e está intrinsecamente ligada à `Carga` que está sendo transportada.
*   **Atualização:** Uma `Viagem` só pode ser atualizada se não estiver vinculada a um `CTe`. Isso evita a modificação de informações da viagem após a emissão do CT-e.

**3. Despesa Adicional:**

*   **Criação:** Uma `DespesaAdicional` deve estar associada a uma `Viagem` existente. Ela representa os custos extras incorridos durante a viagem (pedágios, alimentação, etc.).

**4. CT-e (Conhecimento de Transporte Eletrônico):**

*   **Criação:** Para criar um `CTe`, é necessário que exista uma `Viagem` previamente cadastrada. O `CTe` é o documento fiscal que formaliza o transporte da carga.
    *   **Aliquota:** A alíquota do ICMS (Imposto sobre Circulação de Mercadorias e Serviços) deve estar entre 1% e 100%.
*   **Atualização:** Um `CTe` não pode ser atualizado após a sua criação. Isso garante a integridade e a imutabilidade do documento fiscal.

**Diagrama Simplificado:**

|       |              |                        |
| :---- | :----------- | :---------------       |
| Carga | -> Viagem    | <-- DespesaAdicional   |
|       |         ▲    |                        |
|       |         │    |                        |
|       |        CTe   |                        |

Este diagrama ilustra as dependências entre as entidades. A `Viagem` depende da `Carga`, e tanto a `DespesaAdicional` quanto o `CTe` dependem da `Viagem`.


## Tecnologias Utilizadas

*   **ASP.NET Core:** Framework para a construção da API.
*   **C#:** Linguagem de programação.
*   **Entity Framework Core (EF Core):** ORM para acesso ao banco de dados.
*   **SQL Server (ou outro banco de dados):** Banco de dados relacional para armazenar os dados.
*   **xUnit:** Framework para testes unitários.
*   **Swashbuckle/Swagger:** Para geração da documentação da API e interface de testes.

## Pré-requisitos

Antes de começar, você precisará ter as seguintes ferramentas instaladas:

*   [.NET SDK](https://dotnet.microsoft.com/download) (versão X.X ou superior)
*   [Visual Studio](https://visualstudio.microsoft.com/downloads/) ou [Visual Studio Code](https://code.visualstudio.com/) com a extensão C#
*   [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou outro banco de dados de sua preferência)
*   [NuGet Package Manager](https://www.nuget.org/) (geralmente instalado com o Visual Studio)

## Configuração

1.  **Clone o repositório:**

    ```bash
    git clone https://github.com/murilojcavalcanti/RegistrosCT-e.git
    ```

2.  **Configure a string de conexão do banco de dados:**
    *   Abra o arquivo `appsettings.json`.
    *   Localize a seção de configuração da string de conexão (`ConnectionStrings`).
    *   Caso esteja usando uma forma de autenticação diferente em seu SQL Server modifique a string de conexão para apontar para o seu servidor. Exemplo:

        ```json
        "ConnectionStrings": {
           "cteApp": "Server=localhost;Database=cteAppDb; integrated Security=true; trustServerCertificate=true;""
        }
        ```

3.  **Execute as migrações do EF Core:**

    *   Abra o console na pasta ResgistroCTe.Infra.
    *   Execute o seguinte comando para aplicar as migrações e criar o banco de dados:

        ```powershell
        dotnet ef database update -s ..\RegistrosCTe.API\
        ```

## Como Executar a API

1.  **Abra o projeto no Visual Studio ou Visual Studio Code.**
2.  **Restaure as dependências NuGet:**

    *   Visual Studio: Build -> Restore NuGet Packages
    *   Visual Studio Code: O VS Code geralmente restaura automaticamente ou pede para restaurar.

3.  **Execute a aplicação:**

    *   Visual Studio: Clique no botão "Start" (geralmente com um ícone de play).
    *   Visual Studio Code: Pressione `Ctrl+Shift+B` (para executar a task de build) e depois execute a aplicação a partir do terminal.

4.  **Acesse a documentação do Swagger:**

    *   Após a execução da API, abra um navegador e acesse o endereço `http://localhost:5034/swagger/index.html`.
    *   A interface do Swagger permitirá que você explore os endpoints da API, execute requisições e veja as respostas.

## Testes Unitários

Para executar os testes unitários:

1.  **Abra o Test Explorer no Visual Studio (Test -> Test Explorer).**
2.  **Clique em "Run All" para executar todos os testes.**

Certifique-se de que todos os testes passem antes de fazer qualquer alteração no código.

## Endpoints da API

A API possui os seguintes endpoints:

*   `POST   /api/Viagem`        - Cria uma nova viagem.
*   `GET    /api/Viagem`        - Lista todas as viagens.
*   `GET    /api/Viagem/{id}`    - Obtém uma viagem por ID.
*   `PUT    /api/Viagem/{id}`    - Atualiza uma viagem existente.
*   `DELETE /api/Viagem/{id}`    - Exclui uma viagem.

*   `POST   /api/Carga`        - Cria uma nova carga.
*   `GET    /api/Carga`        - Lista todas as cargas.
*   `GET    /api/Carga/{id}`    - Obtém uma carga por ID.
*   `PUT    /api/Carga/{id}`    - Atualiza uma carga existente.
*   `DELETE /api/Carga/{id}`    - Exclui uma carga.

*   `POST   /api/CTe`           - Cria um novo CT-e.
*   `GET    /api/CTe`           - Lista todos os CT-e.
*   `GET    /api/CTe/{id}`       - Obtém um CT-e por ID.
*   `PUT    /api/CTe/{id}`       - Atualiza um CT-e existente. (Não implementado no exemplo original, mas você pode adicionar)
*   `DELETE /api/CTe/{id}`       - Exclui um CT-e.

*   `POST   /api/DespesasAdicionais` - Cria uma nova despesa adicional.
*   `GET    /api/DespesasAdicionais` - Lista todas as despesas adicionais.
*   `GET    /api/DespesasAdicionais/{id}` - Obtém uma despesa adicional por ID.
*   `PUT    /api/DespesasAdicionais/{id}` - Atualiza uma despesa adicional existente.
*   `DELETE /api/DespesasAdicionais/{id}` - Exclui uma despesa adicional.