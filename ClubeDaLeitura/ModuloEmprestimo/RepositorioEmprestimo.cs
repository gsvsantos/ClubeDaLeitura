namespace ClubeDaLeitura.ModuloEmprestimo;

public class RepositorioEmprestimo
{
    public Emprestimo[] Emprestimos = new Emprestimo[100];
    public int IndiceListaEmprestimo = 0;
    public bool ListaVazia = false;

    public void RegistrarEmprestimo(Emprestimo novoEmprestimo)
    {
        novoEmprestimo.GerarId();
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
        emprestimoEscolhido.Data = dadosEditados.Data;
        emprestimoEscolhido.Situacao = dadosEditados.Situacao;
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
}
