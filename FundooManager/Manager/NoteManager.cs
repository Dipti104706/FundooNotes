using FundooManager.Interface;
using FundooModels;
using FundooRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooManager.Manager
{
    public class NoteManager : INoteManager
    {
        private readonly INoteRepository noteRepository;

        public NoteManager(INoteRepository noteRepository)
        {
            this.noteRepository = noteRepository;
        }

        public string CreateNote(NoteModel notesModel)
        {
            try
            {
                return this.noteRepository.CreateNote(notesModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
