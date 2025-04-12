using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class TelaEmprestimo
{
    public RepositorioAmigo RepositorioAmigo;
    public RepositorioRevista RepositorioRevista;
    public RepositorioEmprestimo RepositorioEmprestimo;

    public TelaEmprestimo(RepositorioEmprestimo repositorioEmprestimo, RepositorioAmigo repositorioAmigo, RepositorioRevista repositorioRevista)
    {
        RepositorioEmprestimo = repositorioEmprestimo;
        RepositorioAmigo = repositorioAmigo;
        RepositorioRevista = repositorioRevista;
    }
}
