using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.Repository;

namespace BLL
{
    public class StudyMaterialBll
    {
        IGenericRepository<StudyMaterial> _studyMaterial = new GenericRepository<StudyMaterial>();
        IGenericRepository<StudyMaterialFile> _studyMaterialFile = new GenericRepository<StudyMaterialFile>();
        public List<StudyMaterial> GetStudyMaterial()
        {
            return _studyMaterial.FindBy(x => true).ToList();
        }
        public StudyMaterial GetStudyMaterialById(int id)
        {
            return _studyMaterial.GetById(id);
        }
        public int AddStudyMaterial(StudyMaterial studyMaterial)
        {
            return _studyMaterial.Add(studyMaterial);
        }
        public int EditStudyMaterial(StudyMaterial studyMaterial)
        {
            return _studyMaterial.Update(studyMaterial);
        }
        public int DeleteStudyMaterial(int id)
        {
             List<StudyMaterialFile> studyMaterialFileList = _studyMaterialFile.FindBy(x => x.StudyMaterialId == id).ToList();
            if (studyMaterialFileList != null)
            {
                _studyMaterialFile.DeleteRange(studyMaterialFileList);
            }
            return _studyMaterial.Delete(_studyMaterial.GetById(id));
        }
        public int AddStudyMaterialFile(StudyMaterialFile file)
        {
            return _studyMaterialFile.Add(file);
        }

        public int DeleteStudyMaterialFile(int id)
        {
            StudyMaterialFile studyMaterialFile = _studyMaterialFile.FindBy(x => x.StudyMaterialId == id).FirstOrDefault();
            if (studyMaterialFile != null)
            {
                _studyMaterialFile.Delete(studyMaterialFile);
            }
            return _studyMaterial.Delete(_studyMaterial.GetById(id));
        }
        public List<StudyMaterial> GetMyStudyMaterial(long id)
        {
            return _studyMaterial.FindBy(x => x.UserId==id).ToList();
        }
    }
}
