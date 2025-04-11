using ClubeDaLeitura.Compartilhado;
using ClubeDaLeitura.ModuloRevista;

namespace ClubeDaLeitura.ModuloCaixa;

public class Caixa : Entidade
{
    public string Etiqueta;
    public string Cor;
    public int DiasEmprestimo;
    public Revista[] Revistas;
    private static int id = 0;
}
