using ClubeDaLeitura.Utils;

namespace ClubeDaLeitura.Compartilhado;

public abstract class TelaBase<T> where T : EntidadeBase<T>
{
    protected string NomeEntidade;
    private RepositorioBase<T> Repositorio;

    protected TelaBase(string nomeEntidade, RepositorioBase<T> repositorio)
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

        T novoRegistro = ObterDados();

        string erros = novoRegistro.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            return;
        }

        Repositorio.CadastrarRegistro(novoRegistro);

        Notificador.ExibirMensagem($"\n{NomeEntidade} registrado com sucesso!", ConsoleColor.Green);
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

        T registroEscolhido = Repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        if (registroEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID inserido não está registrado.", ConsoleColor.Red);
            return;
        }

        T dadosEditados = ObterDados();

        string erros = dadosEditados.Validar();

        if (erros.Length > 0)
        {
            Notificador.ExibirMensagem(erros, ConsoleColor.Red);
            EditarRegistro();
            return;
        }

        Repositorio.EditarRegistro(registroEscolhido, dadosEditados);

        Notificador.ExibirMensagem($"\n{NomeEntidade} editado com sucesso!", ConsoleColor.Green);
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
            ColorirEscrita.SemQuebraLinha("Digite o ID do registro que deseja editar: ");
            idValido = int.TryParse(Console.ReadLine(), out idRegistroEscolhido);

            if (!idValido)
            {
                Notificador.ExibirMensagem("\nO ID inserido é inválido!", ConsoleColor.Red);
                return;
            }
        } while (!idValido);

        T registroEscolhido = Repositorio.SelecionarRegistroPorId(idRegistroEscolhido);

        if (registroEscolhido == null)
        {
            Notificador.ExibirMensagem("\nO ID escolhido não está registrado.", ConsoleColor.Red);
            return;
        }

        Repositorio.ExcluirRegistro(registroEscolhido);

        Notificador.ExibirMensagem("\nAmigo excluído com sucesso!", ConsoleColor.Green);
    }
    public abstract T ObterDados();
}
