using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class RepositorioEmprestimoEmArquivo : RepositorioBaseEmArquivo<Emprestimo>, IRepositorioEmprestimo
{
    public RepositorioEmprestimoEmArquivo(ContextoDados contexto) : base(contexto) { }
    public override void CadastrarRegistro(Emprestimo novoRegistro)
    {
        novoRegistro.Revista.Emprestar();
        novoRegistro.Amigo.ReceberEmprestimo(novoRegistro);
        base.CadastrarRegistro(novoRegistro);
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
        Contexto.SalvarContexto();
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
    protected override List<Emprestimo> ObterListaContexto()
    {
        return Contexto.RegistroEmprestimos;
    }
}
