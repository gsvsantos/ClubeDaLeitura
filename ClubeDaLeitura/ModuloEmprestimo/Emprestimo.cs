using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class Emprestimo : Entidade
{
    public Amigo Amigo;
    public Revista Revista;
    public DateTime Data;
    public string Situacao;
    private static int id = 0;

    public Emprestimo(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        Data = DateTime.Now;
        Situacao = "Aberto";
    }
    public void GerarId()
    {
        Id = ++id;
    }
    public string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "\nVocê precisa selecionar ao menos um Amigo.\n";

        if (Revista == null)
            erros += "Você precisa selecionar ao menos uma Revista.\n";

        if (string.IsNullOrEmpty(Situacao))
            erros += "Campo 'Situacao' é obrigatório.\n";
        else
        {
            if (Situacao != "Aberto" && Situacao != "Concluído")
                erros += "Campo 'Situacao' precisa ser 'Aberto' ou 'Concluído'!\n";
        }

        return erros;
    }
    public void RegistrarDevolucao()
    {
        Situacao = "Concluído";
        Revista.Devolver();
    }
    public DateTime ObterDataDevolucao()
    {
        return Data.AddDays(Revista.Caixa.DiasEmprestimo);
    }
}
