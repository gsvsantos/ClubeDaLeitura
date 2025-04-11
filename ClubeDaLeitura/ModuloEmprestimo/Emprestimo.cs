using ClubeDaLeitura.ModuloAmigo;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloEmprestimo;

public class Emprestimo
{
    public Amigo Amigo;
    public Revista Revista;
    public DateTime Data;
    public string Situacao;
    private static int id = 0;
}
