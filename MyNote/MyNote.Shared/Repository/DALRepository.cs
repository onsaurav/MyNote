using MyNote.Common;
using MyNote.DB;
using MyNote.Helper;
using MyNote.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace MyNote.Repository
{
    public class DALRepository
    {
        DBAccess _DBAccess = new DBAccess();

        public async Task<MyResult> DeleteNote(int _DeletingId)
        {
            MyResult _MyResult = await _DBAccess.DeleteNoteAsync(_DeletingId);
            return _MyResult;
        }
    }
}
