using System.Collections;

namespace ClubeDaLeitura.Compartilhado;

public abstract class RepositorioBase
{
    public ArrayList Registros { get; protected set; } = new ArrayList();
    private int id = 0;
    public bool ListaVazia { get; set; } = false;

    public virtual void CadastrarRegistro(EntidadeBase novoRegistro)
    {
        novoRegistro.Id = ++id;
        Registros.Add(novoRegistro);
    }
    public ArrayList PegarListaRegistrados()
    {
        return Registros;
    }
    public void EditarRegistro(EntidadeBase registroEscolhido, EntidadeBase dadosEditados)
    {
        registroEscolhido.AtualizarRegistro(dadosEditados);
    }
    public void ExcluirRegistro(EntidadeBase registroEscolhido)
    {
        foreach (EntidadeBase registro in Registros)
        {
            if (registroEscolhido == registro)
                Registros.Remove(registroEscolhido);
        }
    }
    public EntidadeBase SelecionarRegistroPorId(int idRegistroEscolhida)
    {
        foreach (EntidadeBase e in Registros)
        {
            if (e.Id == idRegistroEscolhida)
                return e;
        }

        return null!;
    }
}
