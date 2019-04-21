using MyNote.Common;
using MyNote.Model;
using MyNote.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace MyNote.DB
{
    public class DBAccess
    {
        #region DB
        public async Task<bool> CheckDbAsync()
        {
            try
            {
                StorageFile sf = await ApplicationData.Current.LocalFolder.GetFileAsync(Common.Common.DB_NAME);
                return true;
            }
            catch (Exception)
            {
                DropTableAsync();
                return false;
            }
        }
                
        public async Task CreateDatabaseAsync()
        {
            bool isDBExist = await CheckDbAsync();
            if (!isDBExist)
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
                await conn.CreateTableAsync<Note>();
                await conn.CreateTableAsync<User>();
            }
        }

        public async void DropTableAsync()
        {
            try {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
                await conn.DropTableAsync<Note>();
                await conn.DropTableAsync<User>();
            }
            catch (Exception)
            { }
        }
        #endregion

        #region Note
        public async void AddNoteAsync(Note _Note)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            await conn.InsertAsync(_Note);
        }

        public async Task<MyResult> SaveNoteAsync(Note _Note)
        {
            try
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
                var note = await conn.Table<Note>().Where(x => x.Id == _Note.Id).FirstOrDefaultAsync();
                if (note != null)
                {
                    note.Subject = _Note.Subject;
                    note.Date = _Note.Date;
                    note.Time = _Note.Time;
                    note.Latitude = _Note.Latitude;
                    note.Longitude = _Note.Longitude;
                    note.PhotoPath = _Note.PhotoPath;
                    note.Text = _Note.Text;
                    if (_Note.Photo != null)
                        note.Photo = _Note.Photo;
                    await conn.UpdateAsync(note);
                }
                else
                {
                    await conn.InsertAsync(_Note);
                }

                return new MyResult { IsSuccess = true, Message = "Note saved successfully" };
            }
            catch (Exception ex)
            {
                return new MyResult { IsSuccess = false, Message = ex.Message };
            }
        }

        private async Task AddNoteListAsync(List<Note> list)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            await conn.InsertAllAsync(list);
        }

        public async Task<List<Note>> LoadNotesAsync()
        {
            List<Note> list = new List<Note>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);

            var query = conn.Table<Note>();
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                list.Add(new Note
                {
                    Id = item.Id,
                    Subject = item.Subject,
                    Date = item.Date,
                    Time = item.Time,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    PhotoPath = item.PhotoPath,
                    Text = item.Text,
                    Photo = item.Photo
                });
            }
            return list;
        }

        public async Task<List<NoteView>> LoadNotesByUserAsync()
        {
            List<NoteView> listView = new List<NoteView>();
            List<Note> list = await SearchNoteByUserAsync();
            foreach (var item in list)
            {
                DateTime Notedate = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                Notedate = Notedate.AddHours(item.Time.Hour).AddMinutes(item.Time.Minute).AddHours(item.Time.Second);

                listView.Add(new NoteView
                {
                    Id = item.Id,
                    Subject = item.Subject,
                    Date = item.Date,
                    Time = item.Time,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    PhotoPath = item.PhotoPath,
                    Text = item.Text,
                    Photo = item.Photo,
                    NoteDate = Notedate.ToString("dd/MM/yyyy HH:m:ss")
                });
            }
            return listView;
        }

        public async Task<List<Note>> SearchNoteByUserAsync()
        {
            List<Note> list = new List<Note>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);

            var query = conn.Table<Note>();
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                list.Add(new Note
                {
                    Id = item.Id,
                    Subject = item.Subject,
                    Date = item.Date,
                    Time = item.Time,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude,
                    PhotoPath = item.PhotoPath,
                    Text = item.Text,
                    Photo = item.Photo
                });
            }

            #region Other ways to loading
            //var allNotes = await conn.QueryAsync<Note>("SELECT * FROM Notes");
            //foreach (var item in allNotes)
            //{
            //    list.Add(new Note { Id = item.Id, Subject = item.Subject, Date = item.Date, Tags = item.Tags, Time = item.Time, Latitude = item.Latitude, Longitude = item.Longitude, PhotoPath = item.PhotoPath, Text = item.Text });
            //}

            //var mynotes = await conn.QueryAsync<Note>("SELECT Name FROM Notes WHERE Subject = ?", new object[] { "Rome, Italy" });
            //foreach (var item in mynotes)
            //{
            //    list.Add(new Note { Id = item.Id, Subject = item.Subject, Date = item.Date, Tags = item.Tags, Time = item.Time, Latitude = item.Latitude, Longitude = item.Longitude, PhotoPath = item.PhotoPath, Text = item.Text });
            //}
            #endregion

            return list;
        }

        public async Task UpdateNoteAsync(Note item)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            var note = await conn.Table<Note>().Where(x => x.Id == item.Id).FirstOrDefaultAsync();
            if (note != null)
            {
                note.Subject = item.Subject;
                note.Date = item.Date;
                note.Time = item.Time;
                note.Latitude = item.Latitude;
                note.Longitude = item.Longitude;
                note.PhotoPath = item.PhotoPath;
                note.Text = item.Text;
                note.Photo = item.Photo;
                await conn.UpdateAsync(note);
            }
        }

        public async Task<MyResult> DeleteNoteAsync(long id)
        {
            MyResult _MyResult = new MyResult { IsSuccess = false, Message = "" };
            try
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
                var note = await conn.Table<Note>().Where(x => x.Id == id).FirstOrDefaultAsync();
                if (note != null)
                {
                    await conn.DeleteAsync(note);

                    _MyResult.IsSuccess = true;
                    _MyResult.Message = "Note deleted successfully";
                }
                else
                {
                    _MyResult.IsSuccess = false;
                    _MyResult.Message = "Invalid note selected.";
                }
            }
            catch (Exception ex)
            {
                _MyResult.IsSuccess = false;
                _MyResult.Message = ex.Message;
            }

            return _MyResult;
        }
        #endregion

        #region User
        public async void AddUserAsync(User _User)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            await conn.InsertAsync(_User);
        }

        public async Task<MyResult> SaveUserAsync(User _User)
        {
            try
            {
                SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
                var User = await conn.Table<User>().Where(x => (x.Id == _User.Id)).FirstOrDefaultAsync();
                if (User != null)
                {
                    if (User.Id == _User.Id)
                        return await UpdateUserAsync(_User);
                    else
                        return new MyResult { IsSuccess = false, Message = "User name already exist!" };
                }
                else
                    await conn.InsertAsync(_User);

                return new MyResult { IsSuccess = true, Message = "User saved successfully" };
            }
            catch (Exception ex)
            {
                return new MyResult { IsSuccess = false, Message = ex.Message };
            }
        }

        private async Task AddUserListAsync(List<User> list)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            await conn.InsertAllAsync(list);
        }

        public async Task<List<User>> LoadUsersAsync()
        {
            List<User> list = new List<User>();
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);

            var query = conn.Table<User>();
            var result = await query.ToListAsync();
            foreach (var item in result)
            {
                list.Add(new User
                {
                    Id = item.Id,
                    Name = item.Name,
                    DateOfBirth = item.DateOfBirth,
                    Details = item.Details,
                    PhotoPath = item.PhotoPath,
                    Photo = item.Photo
                });
            }
            return list;
        }
        public async Task<User> SearchUserByNameAsync()
        {
            User _User = null;
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);

            var query = conn.Table<User>();
            var result = await query.ToListAsync();
            if (result.Count > 0)
            {
                _User = new User();
                _User.Id = result[0].Id;
                _User.Name = result[0].Name;
                _User.DateOfBirth = result[0].DateOfBirth;
                _User.Details = result[0].Details;
                _User.PhotoPath = result[0].PhotoPath;
                _User.Photo = result[0].Photo;
            }
            return _User;
        }

        public async Task<MyResult> UpdateUserAsync(User item)
        {
            MyResult _MyResult = new MyResult { IsSuccess = false, Message = string.Empty };
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            var User = await conn.Table<User>().Where(x => x.Id == item.Id).FirstOrDefaultAsync();
            if (User != null)
            {
                var DuplicateUserWithName = await conn.Table<User>().Where(x => x.Id != User.Id).FirstOrDefaultAsync();
                if (DuplicateUserWithName == null)
                {
                    User.Name = item.Name;
                    User.DateOfBirth = item.DateOfBirth;
                    User.Details = item.Details;
                    User.PhotoPath = item.PhotoPath;
                    User.Photo = item.Photo;
                    await conn.UpdateAsync(User);

                    _MyResult.IsSuccess = true;
                    _MyResult.Message = "Profile updated successfully.";
                }
                else
                {
                    _MyResult.IsSuccess = false;
                    _MyResult.Message = "User name is duplicate.";
                }
            }
            else
            {
                _MyResult.IsSuccess = false;
                _MyResult.Message = "User not found with this id.";
            }

            return _MyResult;
        }

        public async Task DeleteUserAsync(long id)
        {
            SQLiteAsyncConnection conn = new SQLiteAsyncConnection(Common.Common.DB_NAME);
            var User = await conn.Table<User>().Where(x => x.Id == id).FirstOrDefaultAsync();
            if (User != null)
            {
                await conn.DeleteAsync(User);
            }
        }
        #endregion

        #region Image
        
        #endregion
    }
}
