using APIbackprojeto.Models;

namespace APIbackprojeto.Interfaces
{
    public interface IMarcaRepository 
    {

        // R - Read: devolve todos os cursos.
        List<Marca> ListarTodos();

        // R - Read: busca um curso pelo Id (ou null se não achar).
        Marca BuscarPorId(int id);

        // C - Create: cadastra um curso novo.
        void Cadastrar(Marca marca);
    }
}
    
