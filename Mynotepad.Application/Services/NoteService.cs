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

        public NoteDTO CreateOne(NoteRequest noteRequest, int UserId)
        {
            var note = _mapper.Map<NoteDTO>(noteRequest);
            note.UserId = UserId;
            var result = _repository.Create(_mapper.Map<Note>(note));
            return _mapper.Map<NoteDTO>(result);
        }

        public void DeleteById(int id, int userId)
        {
            var note = _repository.GetById(id);

            if (note == null)
                throw new KeyNotFoundException("Note not found");

            if (note.UserId != userId)
                throw new UnauthorizedAccessException("The logged user doesn't own this note");

            _repository.Delete(id);
        }

        public List<NoteDTO> GetAll(int userId)
        {
            var notes = _repository.GetAllByUserId(userId);
            return _mapper.Map<List<NoteDTO>>(notes);
        }

        public NoteDTO GetById(int id, int userId)
        {
            var note = _repository.GetById(id);

            if (note.UserId != userId)
                throw new UnauthorizedAccessException("The logged user doesn't own this note");
            
            return _mapper.Map<NoteDTO>(note);
        }

        public NoteDTO UpdateOne(NoteRequest noteRequest, int userId)
        {
            // Check if the logged user owns the note that is being updated
            _ = GetById((int)noteRequest.Id!, userId);

            var note = _mapper.Map<NoteDTO>(noteRequest);

            var updatedNote = _repository.Update(_mapper.Map<Note>(note));
            return _mapper.Map<NoteDTO>(updatedNote);
        }
    }
}
