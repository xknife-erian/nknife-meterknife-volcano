using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace MeterKnife.Utilities
{
    /// <summary>
    /// ͼƬ����
    /// http://www.cnblogs.com/wu-jian/
    /// 
    /// �⽣ 2011-02-20 ����
    /// �⽣ 2012-08-08 �޸�
    /// </summary>
    public class ImageUtil
    {
        #region �����Ͳü�������

        /// <summary>
        /// �����Ͳü�
        /// ��ͼƬ����Ϊ���ģ���ȡ�����ͣ�Ȼ��ȱ�����
        /// ����ͷ����
        /// </summary>
        /// <remarks>�⽣ 2012-08-08</remarks>
        /// <param name="fromFile">ԭͼStream����</param>
        /// <param name="fileSaveUrl">����ͼ��ŵ�ַ</param>
        /// <param name="side">ָ���ı߳��������ͣ�</param>
        /// <param name="quality">��������Χ0-100��</param>
        public static void CutForSquare(System.IO.Stream fromFile, string fileSaveUrl, int side, int quality)
        {
            //����Ŀ¼
            string dir = Path.GetDirectoryName(fileSaveUrl);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //ԭʼͼƬ����ȡԭʼͼƬ�������󣬲�ʹ������Ƕ�����ɫ������Ϣ��
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //ԭͼ��߾�С��ģ�棬��������ֱ�ӱ���
            if (initImage.Width <= side && initImage.Height <= side)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //ԭʼͼƬ�Ŀ���
                int initWidth = initImage.Width;
                int initHeight = initImage.Height;

                //���������Ȳü�Ϊ������
                if (initWidth != initHeight)
                {
                    //��ͼ����
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //����ڸߵĺ�ͼ
                    if (initWidth > initHeight)
                    {
                        //����ʵ����
                        pickedImage = new System.Drawing.Bitmap(initHeight, initHeight);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //��������
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //��λ
                        Rectangle fromR = new Rectangle((initWidth - initHeight) / 2, 0, initHeight, initHeight);
                        Rectangle toR = new Rectangle(0, 0, initHeight, initHeight);
                        //��ͼ
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //���ÿ�
                        initWidth = initHeight;
                    }
                    //�ߴ��ڿ����ͼ
                    else
                    {
                        //����ʵ����
                        pickedImage = new System.Drawing.Bitmap(initWidth, initWidth);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);
                        //��������
                        pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        //��λ
                        Rectangle fromR = new Rectangle(0, (initHeight - initWidth) / 2, initWidth, initWidth);
                        Rectangle toR = new Rectangle(0, 0, initWidth, initWidth);
                        //��ͼ
                        pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);
                        //���ø�
                        initHeight = initWidth;
                    }

                    //����ͼ���󸳸�ԭͼ
                    initImage = (System.Drawing.Image) pickedImage.Clone();
                    //�ͷŽ�ͼ��Դ
                    pickedG.Dispose();
                    pickedImage.Dispose();
                }

                //����ͼ����
                System.Drawing.Image resultImage = new System.Drawing.Bitmap(side, side);
                System.Drawing.Graphics resultG = System.Drawing.Graphics.FromImage(resultImage);
                //��������
                resultG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                resultG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //��ָ������ɫ��ջ���
                resultG.Clear(Color.White);
                //��������ͼ
                resultG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, side, side), new System.Drawing.Rectangle(0, 0, initWidth, initHeight),
                    System.Drawing.GraphicsUnit.Pixel);

                //�ؼ���������
                //��ȡϵͳ������������,������jpeg,bmp,png,gif,tiff
                ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo ici = null;
                foreach (ImageCodecInfo i in icis)
                {
                    if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                    {
                        ici = i;
                    }
                }
                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long) quality);

                //��������ͼ
                resultImage.Save(fileSaveUrl, ici, ep);

                //�ͷŹؼ���������������Դ
                ep.Dispose();

                //�ͷ�����ͼ��Դ
                resultG.Dispose();
                resultImage.Dispose();

                //�ͷ�ԭʼͼƬ��Դ
                initImage.Dispose();
            }
        }

        #endregion

        #region �Զ���ü�������

        /// <summary>
        /// ָ������ü�
        /// ��ģ��������Χ�Ĳü�ͼƬ��������ģ��ߴ�
        /// </summary>
        /// <remarks>�⽣ 2012-08-08</remarks>
        /// <param name="fromFile">ԭͼStream����</param>
        /// <param name="fileSaveUrl">����·��</param>
        /// <param name="maxWidth">����(��λ:px)</param>
        /// <param name="maxHeight">����(��λ:px)</param>
        /// <param name="quality">��������Χ0-100��</param>
        public static void CutForCustom(System.IO.Stream fromFile, string fileSaveUrl, int maxWidth, int maxHeight, int quality)
        {
            //���ļ���ȡԭʼͼƬ����ʹ������Ƕ�����ɫ������Ϣ
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //ԭͼ��߾�С��ģ�棬��������ֱ�ӱ���
            if (initImage.Width <= maxWidth && initImage.Height <= maxHeight)
            {
                initImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //ģ��Ŀ�߱���
                double templateRate = (double) maxWidth / maxHeight;
                //ԭͼƬ�Ŀ�߱���
                double initRate = (double) initImage.Width / initImage.Height;

                //ԭͼ��ģ�������ȣ�ֱ������
                if (templateRate == initRate)
                {
                    //��ģ���С��������ͼƬ
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight),
                        new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);
                    templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                //ԭͼ��ģ��������ȣ��ü�������
                else
                {
                    //�ü�����
                    System.Drawing.Image pickedImage = null;
                    System.Drawing.Graphics pickedG = null;

                    //��λ
                    Rectangle fromR = new Rectangle(0, 0, 0, 0); //ԭͼ�ü���λ
                    Rectangle toR = new Rectangle(0, 0, 0, 0); //Ŀ�궨λ

                    //��Ϊ��׼���вü�
                    if (templateRate > initRate)
                    {
                        //�ü�����ʵ����
                        pickedImage = new System.Drawing.Bitmap(initImage.Width, (int) System.Math.Floor(initImage.Width / templateRate));
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        //�ü�Դ��λ
                        fromR.X = 0;
                        fromR.Y = (int) System.Math.Floor((initImage.Height - initImage.Width / templateRate) / 2);
                        fromR.Width = initImage.Width;
                        fromR.Height = (int) System.Math.Floor(initImage.Width / templateRate);

                        //�ü�Ŀ�궨λ
                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = initImage.Width;
                        toR.Height = (int) System.Math.Floor(initImage.Width / templateRate);
                    }
                    //��Ϊ��׼���вü�
                    else
                    {
                        pickedImage = new System.Drawing.Bitmap((int) System.Math.Floor(initImage.Height * templateRate), initImage.Height);
                        pickedG = System.Drawing.Graphics.FromImage(pickedImage);

                        fromR.X = (int) System.Math.Floor((initImage.Width - initImage.Height * templateRate) / 2);
                        fromR.Y = 0;
                        fromR.Width = (int) System.Math.Floor(initImage.Height * templateRate);
                        fromR.Height = initImage.Height;

                        toR.X = 0;
                        toR.Y = 0;
                        toR.Width = (int) System.Math.Floor(initImage.Height * templateRate);
                        toR.Height = initImage.Height;
                    }

                    //��������
                    pickedG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    pickedG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                    //�ü�
                    pickedG.DrawImage(initImage, toR, fromR, System.Drawing.GraphicsUnit.Pixel);

                    //��ģ���С��������ͼƬ
                    System.Drawing.Image templateImage = new System.Drawing.Bitmap(maxWidth, maxHeight);
                    System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(templateImage);
                    templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                    templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    templateG.Clear(Color.White);
                    templateG.DrawImage(pickedImage, new System.Drawing.Rectangle(0, 0, maxWidth, maxHeight),
                        new System.Drawing.Rectangle(0, 0, pickedImage.Width, pickedImage.Height), System.Drawing.GraphicsUnit.Pixel);

                    //�ؼ���������
                    //��ȡϵͳ������������,������jpeg,bmp,png,gif,tiff
                    ImageCodecInfo[] icis = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo ici = null;
                    foreach (ImageCodecInfo i in icis)
                    {
                        if (i.MimeType == "image/jpeg" || i.MimeType == "image/bmp" || i.MimeType == "image/png" || i.MimeType == "image/gif")
                        {
                            ici = i;
                        }
                    }
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long) quality);

                    //��������ͼ
                    templateImage.Save(fileSaveUrl, ici, ep);
                    //templateImage.Save(fileSaveUrl, System.Drawing.Imaging.ImageFormat.Jpeg);

                    //�ͷ���Դ
                    templateG.Dispose();
                    templateImage.Dispose();

                    pickedG.Dispose();
                    pickedImage.Dispose();
                }
            }

            //�ͷ���Դ
            initImage.Dispose();
        }

        #endregion

        #region �ȱ�����

        /// <summary>
        /// ͼƬ�ȱ�����
        /// </summary>
        /// <remarks>�⽣ 2012-08-08</remarks>
        /// <param name="fromFile">ԭͼStream����</param>
        /// <param name="savePath">����ͼ��ŵ�ַ</param>
        /// <param name="targetWidth">ָ���������</param>
        /// <param name="targetHeight">ָ�������߶�</param>
        /// <param name="watermarkText">ˮӡ����(Ϊ""��ʾ��ʹ��ˮӡ)</param>
        /// <param name="watermarkImage">ˮӡͼƬ·��(Ϊ""��ʾ��ʹ��ˮӡ)</param>
        public static void ZoomAuto(System.IO.Stream fromFile, string savePath, System.Double targetWidth, System.Double targetHeight, string watermarkText,
            string watermarkImage)
        {
            //����Ŀ¼
            string dir = Path.GetDirectoryName(savePath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            //ԭʼͼƬ����ȡԭʼͼƬ�������󣬲�ʹ������Ƕ�����ɫ������Ϣ��
            System.Drawing.Image initImage = System.Drawing.Image.FromStream(fromFile, true);

            //ԭͼ��߾�С��ģ�棬��������ֱ�ӱ���
            if (initImage.Width <= targetWidth && initImage.Height <= targetHeight)
            {
                //����ˮӡ
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(initImage))
                    {
                        System.Drawing.Font fontWater = new Font("����", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //͸��ͼƬˮӡ
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //��ȡˮӡͼƬ
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //ˮӡ����������ԭʼͼƬ��߾����ڻ����ˮӡͼƬ
                            if (initImage.Width >= wrImage.Width && initImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(initImage);

                                //͸������
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = {colorMap};
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements =
                                {
                                    new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f}, //͸����:0.5
                                    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage,
                                    new Rectangle(initImage.Width - wrImage.Width, initImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0,
                                    wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);

                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //����
                initImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                //����ͼ���߼���
                double newWidth = initImage.Width;
                double newHeight = initImage.Height;

                //����ڸ߻����ڸߣ���ͼ��������
                if (initImage.Width > initImage.Height || initImage.Width == initImage.Height)
                {
                    //��������ģ��
                    if (initImage.Width > targetWidth)
                    {
                        //��ģ�棬�߰���������
                        newWidth = targetWidth;
                        newHeight = initImage.Height * (targetWidth / initImage.Width);
                    }
                }
                //�ߴ��ڿ���ͼ��
                else
                {
                    //����ߴ���ģ��
                    if (initImage.Height > targetHeight)
                    {
                        //�߰�ģ�棬����������
                        newHeight = targetHeight;
                        newWidth = initImage.Width * (targetHeight / initImage.Height);
                    }
                }

                //������ͼ
                //�½�һ��bmpͼƬ
                System.Drawing.Image newImage = new System.Drawing.Bitmap((int) newWidth, (int) newHeight);
                //�½�һ������
                System.Drawing.Graphics newG = System.Drawing.Graphics.FromImage(newImage);

                //��������
                newG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                newG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                //�ñ���ɫ
                newG.Clear(Color.White);
                //��ͼ
                newG.DrawImage(initImage, new System.Drawing.Rectangle(0, 0, newImage.Width, newImage.Height),
                    new System.Drawing.Rectangle(0, 0, initImage.Width, initImage.Height), System.Drawing.GraphicsUnit.Pixel);

                //����ˮӡ
                if (watermarkText != "")
                {
                    using (System.Drawing.Graphics gWater = System.Drawing.Graphics.FromImage(newImage))
                    {
                        System.Drawing.Font fontWater = new Font("����", 10);
                        System.Drawing.Brush brushWater = new SolidBrush(Color.White);
                        gWater.DrawString(watermarkText, fontWater, brushWater, 10, 10);
                        gWater.Dispose();
                    }
                }

                //͸��ͼƬˮӡ
                if (watermarkImage != "")
                {
                    if (File.Exists(watermarkImage))
                    {
                        //��ȡˮӡͼƬ
                        using (System.Drawing.Image wrImage = System.Drawing.Image.FromFile(watermarkImage))
                        {
                            //ˮӡ����������ԭʼͼƬ��߾����ڻ����ˮӡͼƬ
                            if (newImage.Width >= wrImage.Width && newImage.Height >= wrImage.Height)
                            {
                                Graphics gWater = Graphics.FromImage(newImage);

                                //͸������
                                ImageAttributes imgAttributes = new ImageAttributes();
                                ColorMap colorMap = new ColorMap();
                                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);
                                ColorMap[] remapTable = {colorMap};
                                imgAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                                float[][] colorMatrixElements =
                                {
                                    new float[] {1.0f, 0.0f, 0.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 1.0f, 0.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 0.0f, 1.0f, 0.0f, 0.0f},
                                    new float[] {0.0f, 0.0f, 0.0f, 0.5f, 0.0f}, //͸����:0.5
                                    new float[] {0.0f, 0.0f, 0.0f, 0.0f, 1.0f}
                                };

                                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
                                imgAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                gWater.DrawImage(wrImage,
                                    new Rectangle(newImage.Width - wrImage.Width, newImage.Height - wrImage.Height, wrImage.Width, wrImage.Height), 0, 0,
                                    wrImage.Width, wrImage.Height, GraphicsUnit.Pixel, imgAttributes);
                                gWater.Dispose();
                            }
                            wrImage.Dispose();
                        }
                    }
                }

                //��������ͼ
                newImage.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);

                //�ͷ���Դ
                newG.Dispose();
                newImage.Dispose();
                initImage.Dispose();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �ж��ļ������Ƿ�ΪWEB��ʽͼƬ
        /// (ע��JPG,GIF,BMP,PNG)
        /// </summary>
        /// <param name="contentType">HttpPostedFile.ContentType</param>
        /// <returns></returns>
        public static bool IsWebImage(string contentType)
        {
            if (contentType == "image/pjpeg" || contentType == "image/jpeg" || contentType == "image/gif" || contentType == "image/bmp" ||
                contentType == "image/png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

    }
}