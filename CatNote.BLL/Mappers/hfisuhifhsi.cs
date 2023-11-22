using System;
using AutoMapper;
using Bms.BLL.Models;
using Bms.BLL.Models.Abstractions;
using Bms.BLL.Models.Blocks;
using Bms.DAL.Entities;
using Bms.DAL.Enums;
using CatNote.DAL.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Bms.BLL.MapperProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Message, MessageEntity>().ReverseMap();
        CreateMap<Edge, EdgeEntity>().ReverseMap();
        CreateMap<BaseBlockAbstract, BaseBlockEntity>().ReverseMap();
        CreateMap<Project, ProjectEntity>().ReverseMap();
        CreateMap<Company, CompanyEntity>().ReverseMap();
        CreateMap<User, UserEntity>().ReverseMap();
        CreateMap<Position, PositionEntity>().ReverseMap();
        CreateMap<Variable, VariableEntity>().ReverseMap();

        CreateMap<Chat, ChatEntity>().ReverseMap();

        CreateMap<Scenario, ScenarioEntity>()
            .ForMember(x => x.Blocks,
                opt => opt.MapFrom<BlockResolver>());

        CreateMap<ScenarioEntity, Scenario>()
            .ForMember(x => x.Blocks,
                opt => opt.MapFrom<BlockEntityResolver>());
    }
}
private class BlockResolver : IValueResolver<Scenario, ScenarioEntity, ICollection<BaseBlockEntity>>
{
    public ICollection<BaseBlockEntity> Resolve(
        Scenario source,
        ScenarioEntity destination,
        ICollection<BaseBlockEntity> destMember,
        ResolutionContext context)
    {
        return source.Blocks.Select(block => new BaseBlockEntity()
            {
                BlockId = block.BlockId,
                BlockName = block.BlockName,
                Type = block.Type,
                Position = new PositionEntity { X = block.Position!.X, Y = block.Position!.Y },
                TextMessage = block.TextMessage,
                PositionId = block.PositionId,
                ScenarioId = block.ScenarioId
            })
            .ToList();
    }
}

private class BlockEntityResolver : IValueResolver<ScenarioEntity, Scenario, ICollection<BaseBlockAbstract>>
{
    public ICollection<BaseBlockAbstract> Resolve(
        ScenarioEntity source,
        Scenario destination,
        ICollection<BaseBlockAbstract> destMember,
        ResolutionContext context)
    {
        if (source.Blocks == null) return null;

        ICollection<BaseBlockAbstract> resolvedBlocks = new List<BaseBlockAbstract>();

        foreach (var block in source.Blocks)
        {
            switch (block.Type)
            {
                case ScenarioBlockType.First:
                    resolvedBlocks.Add(new FirstBlock()
                    {
                        Id = block.Id,
                        BlockId = block.BlockId,
                        BlockName = block.BlockName,
                        TextMessage = block.TextMessage,
                        Position = new Position(block.Position.X, block.Position.Y),
                        PositionId = block.PositionId,
                        ScenarioId = block.ScenarioId
                    });
                    break;
                case ScenarioBlockType.Text:
                    resolvedBlocks.Add(new TextBlock(block.TextMessage)
                    {
                        Id = block.Id,
                        BlockId = block.BlockId,
                        BlockName = block.BlockName,
                        Position = new Position(block.Position.X, block.Position.Y),
                        PositionId = block.PositionId,
                        ScenarioId = block.ScenarioId
                    });
                    break;
                case ScenarioBlockType.Switch:
                    resolvedBlocks.Add(new SwitchBlock()
                    {
                        Id = block.Id,
                        BlockId = block.BlockId,
                        BlockName = block.BlockName,
                        Position = new Position(block.Position.X, block.Position.Y),
                        TextMessage = block.TextMessage,
                        PositionId = block.PositionId,
                        ScenarioId = block.ScenarioId
                    });
                    break;
                case ScenarioBlockType.Closing:
                    resolvedBlocks.Add(new ClosingBlock()
                    {
                        Id = block.Id,
                        BlockId = block.BlockId,
                        BlockName = block.BlockName,
                        Position = new Position(block.Position.X, block.Position.Y),
                        TextMessage = block.TextMessage,
                        PositionId = block.PositionId,
                        ScenarioId = block.ScenarioId
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return resolvedBlocks;
    }
}
}
