namespace Practica.Repository.IGenericRepository
{
    public interface IGenericRepository<T> where T : class    // Declaración de la interfaz IGenericRepository<T>
    {
        Task<List<T>> Listar();          // Método para listar elementos de un tipo genérico T
        Task<bool> Crear(T modelo);      // Método para crear un elemento de tipo genérico T
        Task<bool> Editar(T modelo);     // Método para editar un elemento de tipo genérico T
        Task<bool> Eliminar(int id);     // Método para eliminar un elemento de tipo genérico T por su ID
    }
}
