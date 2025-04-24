using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloAmigo;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class RepositorioEmprestimo : RepositorioBase<Emprestimo>
{
    public override void CadastrarRegistro(Emprestimo novoRegistro)
    {
        novoRegistro.Revista.Emprestar();
        novoRegistro.Amigo.ReceberEmprestimo(novoRegistro);
        base.CadastrarRegistro(novoRegistro);
    }
    public bool VerificarEmprestimoAtivo(Amigo amigoEscolhido)
    {
        if (amigoEscolhido.Emprestimos == null)
            return false;

        foreach (Emprestimo e in amigoEscolhido.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Aberto" || e.Situacao == "ATRASADO")
                return true;
        }

        return false;
    }
    public void VerificarEmprestimosAtrasados(List<Emprestimo> emprestimosRegistrados)
    {
        foreach (Emprestimo e in emprestimosRegistrados)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Concluído")
                continue;

            if (DateTime.Now > e.ObterDataDevolucao())
                e.Situacao = "ATRASADO";
        }
    }
    public bool VerificarDevolucao(Emprestimo emprestimoEscolhido)
    {
        for (int i = 0; i < Registros.Count; i++)
        {
            if (Registros[i] == null)
                continue;

            if (emprestimoEscolhido.Id == Registros[i].Id && emprestimoEscolhido.Situacao == "Concluído")
                return true;
        }

        return false;
    }
}
