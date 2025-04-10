using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloEmprestimo;

namespace ClubeDaLeitura.ModuloAmigo;

public class Amigo : Entidade
{
    public string Responsavel;
    public string Nome;
    public string Telefone;
    public Emprestimo Emprestimo;
    private static int id = 0;

    public Amigo(string nome, string responsavel, string telefone, Emprestimo emprestimo)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
        Emprestimo = emprestimo;
    }
    public void GerarId()
    {
        Id = ++id;
    }
    public void Validar()
    {
        string erros = "";

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo 'Nome' é obrigatório.\n";

        if (Nome.Length < 3)
            erros += "O campo 'Nome' precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Responsavel))
            erros += "O campo 'Responsavel' é obrigatório.\n";

        if (Responsavel.Length < 3)
            erros += "O campo 'Responsavel' precisa conter ao menos 3 caracteres.\n";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo 'Telefone' é obrigatório.\n";

        if (Telefone.Length < 12)
            erros += "O campo 'Telefone' deve seguir o formato 00 0000-0000.";
    }

    public Emprestimo ObterEmprestimos()
    {
        return Emprestimo;
    }

}
