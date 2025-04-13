using ClubeDaLeitura.ModuloAmigo;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class RepositorioEmprestimo
{
    public Emprestimo[] Emprestimos = new Emprestimo[100];
    public int IndiceListaEmprestimo = 0;
    public bool ListaVazia = false;

    public void RegistrarEmprestimo(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.GerarId();
        novoEmprestimo.Revista.Emprestar();
        novoEmprestimo.Amigo.PegarEmprestimo(novoEmprestimo);
        Emprestimos[IndiceListaEmprestimo++] = novoEmprestimo;
    }
    public Emprestimo[] PegarListaRegistrados()
    {
        return Emprestimos;
    }
    public void EditarEmprestimo(Emprestimo emprestimoEscolhido, Emprestimo dadosEditados)
    {
        emprestimoEscolhido.Amigo = dadosEditados.Amigo;
        emprestimoEscolhido.Revista = dadosEditados.Revista;
    }
    public void ExcluirEmprestimo(Emprestimo emprestimoEscolhido)
    {
        for (int i = 0; i < Emprestimos.Length; i++)
        {
            if (Emprestimos[i] == null)
                continue;

            else if (Emprestimos[i].Id == emprestimoEscolhido.Id)
            {
                Emprestimos[i] = null!;
                break;
            }
        }
    }
    public Emprestimo SelecionarPorId(int idEmprestimoEscolhido)
    {
        foreach (Emprestimo e in Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Id == idEmprestimoEscolhido)
                return e;
        }

        return null!;
    }
    public bool VerificarEmprestimoAtivo(Amigo amigoEscolhido)
    {
        if (amigoEscolhido.Emprestimos == null)
            return false;

        foreach (Emprestimo e in amigoEscolhido.Emprestimos)
        {
            if (e == null)
                continue;

            if (e.Situacao == "Aberto")
                return true;
        }

        return false;
    }
    public void VerificarEmprestimosAtrasados(Emprestimo[] emprestimosRegistrados)
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
        for (int i = 0; i < Emprestimos.Length; i++)
        {
            if (Emprestimos[i] == null)
                continue;

            if (emprestimoEscolhido.Id == Emprestimos[i].Id && emprestimoEscolhido.Situacao == "Concluído")
                return true;
        }

        return false;
    }
}
