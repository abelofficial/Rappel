using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;

namespace API.Application.Commands;
public class UpdateSubtaskCommand : IRequest<SubTaskResponseDto>
{
    public int TodoId { get; set; }
    public int SubTaskId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public class UpdateSubtaskCommandProfiles : Profile
    {
        public UpdateSubtaskCommandProfiles()
        {
            CreateMap<UpdateSubtaskCommand, SubTask>();
        }
    }
}