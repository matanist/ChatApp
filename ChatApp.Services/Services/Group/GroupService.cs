using ChatApp.Data;

namespace ChatApp.Services;

public class GroupService : IGroupService
{
    private readonly IGroupRepository _groupRepository;

    public GroupService(IGroupRepository groupRepository)
    {
        _groupRepository = groupRepository;
    }

    public async Task<Group> AddAsync(Group entity)
    {
        return await _groupRepository.AddAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _groupRepository.CountAsync();
    }

    public async Task<Group> DeleteAsync(Group entity)
    {
        return await _groupRepository.DeleteAsync(entity);
    }

    public async Task<Group> GetByIdAsync(int id)
    {
        return await _groupRepository.GetByIdAsync(id);
    }

    public async Task<IReadOnlyList<Group>> ListAllAsync(PaginationModel paginationModel)
    {
        return await _groupRepository.ListAllAsync(paginationModel);
    }

    public async Task<Group> UpdateAsync(Group entity)
    {
        return await _groupRepository.UpdateAsync(entity);
    }
}
