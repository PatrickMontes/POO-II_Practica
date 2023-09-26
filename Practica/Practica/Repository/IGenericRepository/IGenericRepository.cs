namespace Practica.Repository.IGenericRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> Listar();
        Task<bool> Crear(T modelo);
        Task<bool> Editar(T modelo);
        Task<bool> Eliminar(int id);
    }
}
