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

    public Emprestimo(Amigo amigo, Revista revista, string situacao)
    {
        Amigo = amigo;
        Revista = revista;
        Data = DateTime.Now;
        Situacao = situacao;
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

        if (Revista == null) // Acrescentar verificação da disponibilidade da revista.
            erros += "\nVocê precisa selecionar ao menos uma Revista.";

        if (string.IsNullOrEmpty(Situacao))
            erros += "\nCampo 'Situacao' é obrigatório.";
        else
        {
            if (Situacao != "Aberto" || Situacao != "Concluído")
                erros += "\nCampo 'Situacao' precisa ser 'Aberta' ou 'Concluído'!";
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
