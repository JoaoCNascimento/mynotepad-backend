using AutoMapper;
using MyNotepad.Domain.DTOs;
using MyNotepad.Domain.Entities;
using MyNotepad.Domain.Interfaces.Repositories;
using MyNotepad.Domain.Interfaces.Services;
using MyNotepad.Domain.Requests;

namespace MyNotepad.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _repository;
        private readonly IMapper _mapper;

        public NoteService(INoteRepository repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public NoteDTO CreateOne(NoteRequest note, int UserId)
        {
            var _note = _mapper.Map<NoteDTO>(note);
            var result = _repository.Create(_mapper.Map<Note>(_note));
            return _mapper.Map<NoteDTO>(result);
        }

        public void DeleteById(int id)
        {
            _repository.Delete(id);
        }

        public List<NoteDTO> GetAll(int userId)
        {
            var result = _repository.GetAllByUserId(userId);
            return _mapper.Map<List<NoteDTO>>(result);
        }

        public NoteDTO GetById(int id)
        {
            var result = _repository.GetById(id);
            return _mapper.Map<NoteDTO>(result);
        }

        public NoteDTO UpdateOne(NoteRequest note)
        {
            var _note = _mapper.Map<NoteDTO>(note);
            var result = _repository.Update(_mapper.Map<Note>(_note));
            return _mapper.Map<NoteDTO>(result);
        }
    }
}
