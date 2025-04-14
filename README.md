# Clube Da Leitura
Um programa para gerenciar um clube da leitura, permitindo o cadastro e a organização de amigos, caixas, revistas, e empréstimos de revistas.

![Clube da Leitura](https://github.com/user-attachments/assets/6befd998-beea-49e0-8a0e-ee8de5967723)

## Diagrama do Projeto
Planejamento inicial do projeto: [Trabalho - Clube da Leitura](https://whimsical.com/trabalho-clube-da-leitura-v1-J2scDX6PrtsvtkU3ELiqN4)  
*Nota: a versão atual (branch master) pode conter atualizações que não foram refletidas no diagrama original.*

## Como Usar
1. Menu principal com as seguintes opções:
   - **Gerenciar Amigos:**
      - Registrar Amigo
      - Visualizar Lista de Amigos
      - Visualizar Empréstimos de um Amigo
      - Editar Amigo
      - Excluir Amigo
   - **Gerenciar Caixas:** 
      - Registrar Caixa
      - Visualizar Lista de Caixas
      - Editar Caixa
      - Excluir Caixa
   - **Gerenciar Revistas:**
      - Registrar Revista
      - Visualizar Lista de Revistas
      - Editar Revista
      - Excluir Revista
   - **Gerenciar Empréstimos:**
      - Registrar Empréstimo
      - Visualizar Lista de Empréstimos
      - Editar Empréstimo
      - Excluir Empréstimo
      - Registrar Devolução

*O sistema valida os dados inseridos para garantir que os dados inseridos estejam corretos.*

## Estrutura do Projeto
O projeto está organizado da seguinte forma:

- **[Compartilhado/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/Compartilhado)** Contém classes auxiliares e compartilhadas pelo sistema.
  - **[ColorirEscrita.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/Compartilhado/ColorirEscrita.cs):** Cuida da coloração das impressões no console.
  - **[Entidade.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/Compartilhado/Entidade.cs):** Base para as entidades, ajuda com um melhor gerenciamento dos IDs.
  - **[MenuPrincipal.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/Compartilhado/MenuPrincipal.cs):** Contém o método estático para apresentar o MenuPrincipal do programa.
  - **[Notificador.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/Compartilhado/Notificador.cs):** Contém um método estático para impressão das mensagens de falhas ou conclusões.

- **[ModuloAmigo/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloAmigo)** Módulo responsável pelo cadastro e gerenciamento dos amigos do clube.
  - **[Amigo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloAmigo/Amigo.cs):** Define os atributos e métodos de um amigo.
  - **[RepositorioAmigo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloAmigo/RepositorioAmigo.cs):** Gerencia os dados dos amigos.
  - **[TelaAmigo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloAmigo/TelaAmigo.cs):** Interface para interação com o usuário no gerenciamento dos amigos.

- **[ModuloCaixa/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloCaixa)** Módulo para lidar com a organização das caixas.
  - **[Caixa.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloCaixa/Caixa.cs):** Define os atributos e métodos de uma caixa.
  - **[RepositorioCaixa.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloCaixa/RepositorioCaixa.cs):** Gerencia o armazenamento e a manipulação dos dados das caixas.
  - **[TelaCaixa.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloCaixa/TelaCaixa.cs):** Interface para interface do usuário para o gerenciamento das caixas.

- **[ModuloRevista/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloRevista)** Módulo responsável pelo cadastro e gerenciamento das revistas.
  - **[Revista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/Revista.cs):** Contém os atributos e métodos relacionados às revistas.
  - **[RepositorioRevista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/RepositorioRevista.cs):** Gerencia os dados das revistas.
  - **[TelaRevista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/TelaRevista.cs):** Responsável pela interação com o usuário no gerenciamento das revistas.

- **[ModuloEmprestimo/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloEmprestimo)** Módulo para lidar com o registro e controle dos empréstimos de revistas.
  - **[Emprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/Emprestimo.cs):** Define os atributos e métodos para o gerenciamento dos empréstimos.
  - **[RepositorioEmprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/RepositorioEmprestimo.cs):** Gerencia o armazenamento e a manipulação dos dados dos empréstimos.
  - **[TelaEmprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/TelaEmprestimo.cs):** Interface para interface do usuário para o gerenciamento dos empréstimos.

- **[Program.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/Program.cs)** Arquivo principal que contém o setup e a execução do programa.

## Requisitos

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto.
 
## Tecnologias

[![Tecnologias](https://skillicons.dev/icons?i=git,github,visualstudio,cs,dotnet)](https://skillicons.dev)

## Como Utilizar
1. **Clone o Repositório:**
```
git clone https://github.com/gsvsantos/ClubeDaLeitura.git
```

2. Abra o terminal ou prompt de comando e navegue até a pasta raiz do programa.

3. Utilize o comando abaixo para restaurar as dependências do projeto.
```
dotnet restore
```

4. Compile e execute o arquivo *.exe* do programa.
```
dotnet build --configuration Release
```
```
ClubeDaLeitura.exe
```

### Opcional
- Executar o projeto compilando em tempo real
```
dotnet run --project ClubeDaLeitura
```

# Sobre o Projeto

Este projeto foi desenvolvido como parte de um trabalho da [Academia do Programador](https://www.instagram.com/academiadoprogramador/).
