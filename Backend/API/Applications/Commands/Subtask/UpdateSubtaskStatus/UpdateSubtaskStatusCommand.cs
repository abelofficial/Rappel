using System.ComponentModel.DataAnnotations;
using API.Application.Commands.Dtos;
using API.Application.Results;
using API.Domain.Entities;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace API.Application.Commands;

public class UpdateSubtaskStatusCommand : IRequest<SubTaskResponseDto>
{
    [Required]
    public int TodoId { get; set; }

    [Required]
    public int SubTaskId { get; set; }

    [Required]
    [Newtonsoft.Json.JsonConverter(typeof(StringEnumConverter))]
    public ProgressStatus Status { get; set; }

    public class UpdateSubtaskStatusCommandProfiles : Profile
    {
        public UpdateSubtaskStatusCommandProfiles()
        {
            CreateMap<UpdateSubtaskStatusRequestDto, UpdateSubtaskStatusCommand>();
        }
    }
}