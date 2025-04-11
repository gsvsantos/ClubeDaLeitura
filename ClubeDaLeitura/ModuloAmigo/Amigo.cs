using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloAmigo;

public class Amigo : Entidade
{
    public string Nome;
    public string Responsavel;
    public string Telefone;
    public Emprestimo[] Emprestimos;
    private static int id = 0;

    public Amigo(string nome, string responsavel, string telefone)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
    }
    public void GerarId()
    {
        Id = ++id;
    }
    public string Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "\nO campo 'Nome' é obrigatório.\n";
        else
        {
            if (Nome.Length < 3 || !Nome.All(c => char.IsLetter(c) || c == ' '))
                erros += "O campo 'Nome' precisa conter ao menos 3 caracteres e apenas letras.\n";

            if (Nome.Length > 100)
                erros += "O campo 'Nome' não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Responsavel))
            erros += "O campo 'Responsavel' é obrigatório.\n";
        else
        {
            if (Responsavel.Length < 3 || !Responsavel.All(c => char.IsLetter(c) || c == ' '))
                erros += "O campo 'Responsavel' precisa conter ao menos 3 caracteres.\n";

            if (Responsavel.Length > 100)
                erros += "O campo 'Responsavel' não pode ter mais que 100 caracteres.\n";
        }

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";
        else
        {
            if (Telefone.Length < 12 || Telefone.Length > 13)
                erros += "O campo 'Telefone' deve seguir os formatos 00 0000-0000 ou 00 00000-0000.\n";

            if (Telefone.Length >= 3 && (!char.IsDigit(Telefone[0]) || !char.IsDigit(Telefone[1]) || Telefone[2] != ' '))
                erros += "O DDD deve ter dois números seguidos de um espaço (Exemplo: '51 ...').\n";

            if (Telefone.Length == 12 && Telefone[7] != '-')
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).\n";

            if (Telefone.Length == 13 && Telefone[8] != '-')
                erros += "O campo 'Telefone' está errado! Use os formatos (00 0000-0000 ou 00 00000-0000).\n";

            if (!Telefone.All(n => char.IsDigit(n) || n == ' ' || n == '-'))
                erros += "O campo 'Telefone' deve seguir os formatos 00 0000-0000 ou 00 00000-0000.\n";
        }

        return erros;
    }
    public Emprestimo[] ObterEmprestimos()
    {
        return Emprestimos;
    }
}
