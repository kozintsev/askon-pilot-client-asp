using System;
using System.IO;

namespace Ascon.Pilot.WebClient.ViewModels
{
    /// <summary>
    /// ������ �����
    /// </summary>
    public class FileViewModel
    {
        /// <summary>
        /// ���������� �����.
        /// </summary>
        public string FileExtension
        {
            get { return Path.GetExtension(FileName); }
        }
        /// <summary>
        /// ���������� ������������� �����.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ���������� true �� �������� ������ ������.
        /// </summary>
        public bool IsFolder { get; set; }
        /// <summary>
        /// ���������� true, ���� ��� ������� ������� �������� �����
        /// </summary>
        public bool IsThumbnailAvailable {
            get
            {
                return FileExtension == ".xps" || FileExtension == ".pdf";
            }
        }

        /// <summary>
        /// ���������� ������������� ������-�������, ����������� �������� �������� ������ ����
        /// </summary>
        public Guid ObjectId { get; set; }
        /// <summary>
        /// ������������� ������-�������, ����������� �������� ������ ����
        /// </summary>
        public int ObjectTypeId { get; set; }
        /// <summary>
        /// ��������� ������-�������, ����������� �������� ������ ����
        /// </summary>
        public string ObjectName { get; set; }
        /// <summary>
        /// ��� ������-�������, ����������� �������� ������ ���� � ��� ����������. 
        /// </summary>
        public string Name {
            get
            {
                if (Path.HasExtension(ObjectName) && Path.GetExtension(ObjectName) == FileExtension)
                    return ObjectName;
                return $"{ObjectName}{FileExtension}";
            }
        }
        /// <summary>
        /// ��� ������� �����
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// ������ ������� �����
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// ���� ���������� ��������� �����.
        /// </summary>
        public DateTime LastModifiedDate { get; set; }
        /// <summary>
        /// ���� �������� �����
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// ���������� �������� ���������.
        /// </summary>
        public int ChildrenCount { get; set; }
    }
}