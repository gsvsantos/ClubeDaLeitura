using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class Emprestimo : EntidadeBase<Emprestimo>
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime Data { get; set; }
    public string Situacao { get; set; }

    public Emprestimo() { }
    public Emprestimo(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        Data = DateTime.Now;
        Situacao = "Aberto";
    }
    public override string Validar()
    {
        string erros = "";

        if (Amigo == null)
            erros += "\nO amigo selecionado náo está registrado.\n";
        else
        {
            if (Amigo.VerificarMultas())
                erros += "O amigo selecionado tem multas pendentes.\n";

            if (Amigo.VerificarEmprestimosAbertos())
                erros += "O amigo selecionado tem um empréstimo em aberto.\n";

        }

        if (Revista == null)
            erros += "\nA revista selecionada não está registrada.\n";
        else
        {
            if (Revista.StatusEmprestimo != "Disponível")
                erros += "A revista selecionada não está disponível.\n";

            if (Revista.StatusEmprestimo == "Reservada")
                erros += "A revista selecionada está reservada.\n";
        }

        if (string.IsNullOrEmpty(Situacao))
            erros += "\nCampo 'Situacao' é obrigatório.\n";
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
    public bool VerificarEmprestimoAtivo()
    {
        if (Amigo.Emprestimos == null)
            return false;

        foreach (Emprestimo e in Amigo.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Aberto" || e.Situacao == "ATRASADO")
                return true;
        }

        return false;
    }
    public override void AtualizarRegistro(Emprestimo dadosEditados)
    {
        Amigo = dadosEditados.Amigo;
        Revista = dadosEditados.Revista;
        Situacao = dadosEditados.Situacao;
    }
}
