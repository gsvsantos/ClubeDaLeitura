# Clube Da Leitura
Um programa para gerenciar um clube da leitura, permitindo o cadastro e a organização de amigos, caixas, revistas, empréstimos, multas por atraso e reservas de revistas.

![Clube da Leitura](https://github.com/user-attachments/assets/6befd998-beea-49e0-8a0e-ee8de5967723) 
**gif desatualizado ([V1](https://github.com/gsvsantos/ClubeDaLeitura/tree/v1))*

## Diagrama do Projeto
Planejamento da primeira versão do projeto: [Trabalho - Clube da Leitura v1](https://whimsical.com/trabalho-clube-da-leitura-v1-J2scDX6PrtsvtkU3ELiqN4)  
Planejamento da segunda versão do projeto: [Trabalho - Clube da Leitura v2 (Desafios)](https://whimsical.com/trabalho-clube-da-leitura-v2-desafios-4WWJgpzCCHXwUpTf4ACXXQ)  
*Nota: a versão atual (branch master) pode conter atualizações que não foram refletidas nos diagramas originais.*

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
   - **Gerenciar Multas:**
      - Visualizar Multas Pendentes
      - Pagar Multa
      - Visualizar Multas de um Amigo
   - **Gerenciar Reservas:**
      - Registrar Reserva
      - Cancelar Reserva
      - Visualizar Reservas Ativas
      - Pegar Revista Emprestada

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

- **[ModuloEmprestimo/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloEmprestimo)** Módulo para lidar com o registro e controle dos empréstimos de revistas.
  - **[Emprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/Emprestimo.cs):** Define os atributos e métodos para o gerenciamento dos empréstimos.
  - **[RepositorioEmprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/RepositorioEmprestimo.cs):** Gerencia o armazenamento e a manipulação dos dados dos empréstimos.
  - **[TelaEmprestimo.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloEmprestimo/TelaEmprestimo.cs):** Interface para interface do usuário para o gerenciamento dos empréstimos.
 
- **[ModuloMulta/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloMulta)** Módulo para lidar com a organização das multas.
  - **[Multa.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloMulta/Multa.cs):** Define os atributos e métodos para o gerenciamento das multas.
  - **[RepositorioMulta.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloMulta/RepositorioMulta.cs):** Gerencia o armazenamento e a manipulação dos dados das multas.
  - **[TelaMulta.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloMulta/TelaMulta.cs):** Interface para interface do usuário para o gerenciamento das multas.

- **[ModuloReserva/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloReserva)** Módulo responsável pelo cadastro e gerenciamento das reservas.
  - **[RepositorioReserva.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloReserva/RepositorioReserva.cs):** Gerencia o armazenamento e a manipulação dos dados das reservas.
  - **[Reserva.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloReserva/Reserva.cs):** Define os atributos e métodos para o gerenciamento das reservas.
  - **[TelaReserva.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloReserva/TelaReserva.cs):** Interface para interface do usuário para o gerenciamento das reservas.

- **[ModuloRevista/](https://github.com/gsvsantos/ClubeDaLeitura/tree/master/ClubeDaLeitura/ModuloRevista)** Módulo responsável pelo cadastro e gerenciamento das revistas.
  - **[RepositorioRevista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/RepositorioRevista.cs):** Gerencia os dados das revistas.
  - **[Revista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/Revista.cs):** Contém os atributos e métodos relacionados às revistas.
  - **[TelaRevista.cs](https://github.com/gsvsantos/ClubeDaLeitura/blob/master/ClubeDaLeitura/ModuloRevista/TelaRevista.cs):** Responsável pela interação com o usuário no gerenciamento das revistas.

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
