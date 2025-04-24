using System.Globalization;
using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloCaixa;

namespace ClubeDaLeitura.ModuloRevista;

public class Revista : EntidadeBase<Revista>
{
    public string Titulo;
    public int NumeroEdicao;
    public string AnoPublicacao;
    public string StatusEmprestimo;
    public Caixa Caixa;

    public Revista(string titulo, int numeroEdicao, string anoPublicacao, Caixa caixa)
    {
        Titulo = titulo;
        NumeroEdicao = numeroEdicao;
        AnoPublicacao = anoPublicacao;
        StatusEmprestimo = "Disponível";
        Caixa = caixa;
    }
    public override string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Titulo))
            erros += "\nO campo 'Título' é obrigatório.\n";
        else
        {
            if (Titulo.Length < 2 || !Titulo.All(c => char.IsLetter(c) || c == ' '))
                erros += "O campo 'Titulo' precisa conter ao menos 2 caracteres e apenas letras.\n";

            if (Titulo.Length > 100)
                erros += "O campo 'Titulo' não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(NumeroEdicao.ToString()))
            erros += "O campo 'Número de Edição' é obrigatório.\n";
        else
        {
            if (NumeroEdicao < 0)
                erros += "O campo 'Número de Edição' precisa ser um número positivo.\n";
        }

        if (string.IsNullOrWhiteSpace(AnoPublicacao))
            erros += "O campo 'Ano de Publicação' é obrigatório.\n";
        else
        {
            if (AnoPublicacao.Length != 4 && AnoPublicacao.All(char.IsDigit))
                erros += "O campo 'Ano de Publicação' está inválido! Insira somente o ano (yyyy).\n";

            if (!DateTime.TryParse($"01/01/{AnoPublicacao}", CultureInfo.InvariantCulture, out DateTime anoPublicacao))
                erros += "O campo 'Ano de Publicação' está inválido! Insira somente o ano (yyyy).\n";
            else
            {
                if (anoPublicacao < DateTime.Parse("17/02/1895"))
                    erros += "O campo 'Ano de Publicação' não pode ser anterior a primeira HQ lançada! (1895 - YellowKid).\n";

                if (anoPublicacao > DateTime.Now)
                    erros += "O campo 'Ano de Publicação' não pode ser um ano futurístico.\n";
            }
        }

        if (Caixa == null)
            erros += "A caixa selecionada não está registrada!\n";

        return erros;
    }
    public void Emprestar()
    {
        StatusEmprestimo = "Emprestada";
    }
    public void Devolver()
    {
        StatusEmprestimo = "Disponível";
    }
    public void Reservar()
    {
        StatusEmprestimo = "Reservada";
    }
    public override void AtualizarRegistro(Revista dadosEditados)
    {
        Titulo = dadosEditados.Titulo;
        NumeroEdicao = dadosEditados.NumeroEdicao;
        AnoPublicacao = dadosEditados.AnoPublicacao;
        Caixa = dadosEditados.Caixa;
    }
}
