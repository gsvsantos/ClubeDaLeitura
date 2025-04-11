namespace ClubeDaLeitura.ModuloAmigo;

public class RepositorioAmigo
{
    public Amigo[] Amigos = new Amigo[100];
    public int IndiceListaAmigos = 0;
    public bool ListaVazia = false;

    public void RegistrarAmigo(Amigo novoAmigo)
    {
        novoAmigo.GerarId();
        Amigos[IndiceListaAmigos++] = novoAmigo;
    }
    public Amigo[] PegarListaRegistrados()
    {
        return Amigos;
    }
    public void EditarAmigo(Amigo amigoEscolhido, Amigo dadosEditados)
    {
        amigoEscolhido.Nome = dadosEditados.Nome;
        amigoEscolhido.Responsavel = dadosEditados.Responsavel;
        amigoEscolhido.Telefone = dadosEditados.Telefone;
    }
    public void ExcluirAmigo(Amigo amigoEscolhido)
    {
        for (int i = 0; i < Amigos.Length; i++)
        {
            if (Amigos[i] == null)
                continue;

            else if (Amigos[i].Id == amigoEscolhido.Id)
            {
                Amigos[i] = null!;
                break;
            }
        }
    }
    public Amigo SelecionarPorId(int idAmigoEscolhido)
    {
        foreach (Amigo a in Amigos)
        {
            if (a == null)
                continue;
            if (a.Id == idAmigoEscolhido)
                return a;
        }
        return null!;
    }
}
