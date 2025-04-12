using ClubeDaLeitura.ModuloCaixa;

namespace ClubeDaLeitura.ModuloRevista;

public class TelaRevista
{
    public RepositorioRevista RepositorioRevista;
    public RepositorioCaixa RepositorioCaixa;

    public TelaRevista(RepositorioRevista repositorioRevista, RepositorioCaixa repositorioCaixa)
    {
        RepositorioRevista = repositorioRevista;
        RepositorioCaixa = repositorioCaixa;
    }
}
