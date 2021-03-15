using DTOs;

namespace BL
{
    public interface ISaveData
    {
        void SaveToStorage(Measurement elementToStoreage);
    }
}