namespace BancaApi.Repositories
{
    public interface ICreditoRepository
    {
        Task<IEnumerable<CreditoDto>> GeAllAsync();
        Task<CreditoDto?> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCreditoDto createDto);
        Task<bool> UpdateAsync (int id,UpdateDto updateDto);
        Task<bool> DeleteAsync(int id); 

    }
}
