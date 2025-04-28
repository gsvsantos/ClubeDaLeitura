using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.Compartilhado;

public abstract class TelaBase<TTela> where TTela : EntidadeBase<TTela>
{
    protected string NomeEntidade;
    private IRepositorio<TTela> Repositorio;

    protected TelaBase(string nomeEntidade, IRepositorio<TTela> repositorio)
    {
        NomeEntidade = nomeEntidade;
        Repositorio = repositorio;
    }
    public virtual string ApresentarMenu()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"1 >> Registrar {NomeEntidade}");
        ColorirEscrita.ComQuebraLinha($"2 >> Visualizar Lista de {NomeEntidade}s");
        ColorirEscrita.ComQuebraLinha($"3 >> Editar {NomeEntidade}");
        ColorirEscrita.ComQuebraLinha($"4 >> Excluir {NomeEntidade}");
        ColorirEscrita.ComQuebraLinha("S >> Voltar");

        ColorirEscrita.SemQuebraLinha("\nOpção: ");
        string opcao = Console.ReadLine()!;

        if (opcao == null)
            return null!;
        else
            return opcao.Trim().ToUpper();
    }
    public void ExibirCabecalho()
    {
        Console.Clear();
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");
        ColorirEscrita.ComQuebraLinha($"Gestão de {NomeEntidade}s");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");
    }
    public virtual void CadastrarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"Registrando {NomeEntidade}...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------\n");

        TTela novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        string mensagem;
        if (TemRestricoesNoInserir(novoRegistro, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        Repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem($"\nRegistro cadastrado com sucesso!", ConsoleColor.Green);
    }

    public abstract void MostrarListaRegistrados(bool exibirCabecalho, bool comId);
    public virtual void EditarRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"Editando {NomeEntidade}...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (Repositorio.ListaVazia)
            return;

        bool idValido;
        int idRegistroEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Digite o ID do registro que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID inserido é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idValido);

        TTela registroEscolhido = Repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        if (registroEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID inserido não está registrado.", ConsoleColor.Red);
            return;
        }

        TTela dadosEditados = ObterDados();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            EditarRegistro();
            return;
        }

        string mensagem;
        if (TemRestricoesNoEditar(registroEscolhido, dadosEditados, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        Repositorio.EditarRegistro(registroEscolhido, dadosEditados);

        Notificador.ExibirMensagem($"\nRegistro editado com sucesso!", ConsoleColor.Green);
    }
    public virtual void ExcluirRegistro()
    {
        ExibirCabecalho();

        ColorirEscrita.ComQuebraLinha($"Excluindo {NomeEntidade}...");
        ColorirEscrita.ComQuebraLinha("--------------------------------------------");

        MostrarListaRegistrados(false, true);

        if (Repositorio.ListaVazia)
            return;

        bool idValido;
        int idRegistroEscolhido;

        do
        {
            ColorirEscrita.ComQuebraLinha("\n--------------------------------------------");
            ColorirEscrita.SemQuebraLinha("Digite o ID do registro que deseja excluir: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID inserido é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idValido);

        TTela registroEscolhido = Repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        if (registroEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            return;
        }

        string mensagem;
        if (TemRestricoesNoExcluir(registroEscolhido, out mensagem))
        {
            Notificador.ExibirMensagem(mensagem, ConsoleColor.Red);
            return;
        }

        Repositorio.ExcluirRegistro(registroEscolhido);

        Notificador.ExibirMensagem($"\nRegistro excluído com sucesso!", ConsoleColor.Green);
    }

    public virtual bool TemRestricoesNoInserir(TTela novoRegistro, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    public virtual bool TemRestricoesNoEditar(TTela registroEscolhido, TTela dadosEditados, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    public virtual bool TemRestricoesNoExcluir(TTela registroEscolhido, out string mensagem)
    {
        mensagem = "";

        return false;
    }

    public abstract TTela ObterDados();
}
