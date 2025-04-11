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
}
