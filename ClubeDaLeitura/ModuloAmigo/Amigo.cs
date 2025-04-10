using ClubeDaLeitura.Compartilhado;

namespace ClubeDaLeitura.ModuloAmigo;

public class Amigo : Entidade
{
    public string Responsavel;
    public string Nome;
    public string Telefone;
    private static int id = 0;
    public Amigo(string nome, string responsavel, string telefone)
    {
        Nome = nome;
        Responsavel = responsavel;
        Telefone = telefone;
    }
}
