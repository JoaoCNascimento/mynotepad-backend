using AutoMapper;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MyNotepad.Application.Interfaces;
using MyNotepad.Domain.DTO;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces;

namespace MyNotepad.Application.Services;
public class NoteService : INoteService
{
    private INoteRepository _noteRepository;
    private IMapper _mapper;
    private ILogger<NoteService> _logger;

    public NoteService(INoteRepository noteRepository, IMapper mapper, ILogger<NoteService> logger)
    {
        _noteRepository = noteRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public NoteDTO CreateOne(string userId, NoteDTO obj)
    {
        var result = _noteRepository.CreateOne(new ObjectId(userId), _mapper.Map<Note>(obj));
        return _mapper.Map<NoteDTO>(result);
    }

    public List<NoteDTO> GetAll(string userId)
    {
        var result = _noteRepository.GetAll(new ObjectId(userId));
        return _mapper.Map<List<NoteDTO>>(result);
    }

    public NoteDTO GetOne(string userId, string id)
    {
        var result = _noteRepository.GetOne(new ObjectId(userId), new ObjectId(id));
        return _mapper.Map<NoteDTO>(result);
    }

    public bool RemoveOne(string userId, string id)
    {
        return _noteRepository.RemoveOne(new ObjectId(userId), new ObjectId(id));
    }

    public NoteDTO UpdateOne(string userId, NoteDTO note)
    {
        var result = _noteRepository.UpdateOne(new ObjectId(userId), _mapper.Map<Note>(note));
        return _mapper.Map<NoteDTO>(result);
    }
    
    #region Not implemented
    public List<NoteDTO> GetAll()
    {
        throw new NotImplementedException();
    }
    public NoteDTO GetOne(string id)
    {
        throw new NotImplementedException();
    }
    public bool RemoveOne(string id)
    {
        throw new NotImplementedException();
    }
    public NoteDTO UpdateOne(NoteDTO obj)
    {
        throw new NotImplementedException();
    }

    public NoteDTO CreateOne(NoteDTO obj)
    {
        throw new NotImplementedException();
    }
    #endregion
}