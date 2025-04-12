namespace ClubeDaLeitura.ModuloRevista;

public class RepositorioRevista
{
    public bool ListaVazia = false;
    public Revista[] Revistas = new Revista[100];
    public int IndiceListaRevista = 0;

    public void RegistrarRevista(Revista novaRevista)
    {
        novaRevista.GerarId();
        Revistas[IndiceListaRevista++] = novaRevista;
    }
    public Revista[] PegarListaRegistrados()
    {
        return Revistas;
    }
    public void EditarRevista(Revista revistaEscolhida, Revista dadosEditados)
    {
        revistaEscolhida.Titulo = dadosEditados.Titulo;
        revistaEscolhida.NumeroEdicao = dadosEditados.NumeroEdicao;
        revistaEscolhida.AnoPublicacao = dadosEditados.AnoPublicacao;
    }
    public void ExcluirRevista(Revista revistaEscolhida)
    {
        for (int i = 0; i < Revistas.Length; i++)
        {
            if (Revistas[i] == null)
                continue;

            else if (Revistas[i].Id == revistaEscolhida.Id)
            {
                Revistas[i] = null!;
                break;
            }
        }
    }
    public Revista SelecionarPorId(int idRevistaEscolhida)
    {
        foreach (Revista r in Revistas)
        {
            if (r == null)
                continue;
            if (r.Id == idRevistaEscolhida)
                return r;
        }
        return null!;
    }
    public bool VerificarRevistaDisponivel(Revista revista)
    {
        if (revista.StatusEmprestimo == "Disponível")
            return true;
        else
            return false;
    }
}
