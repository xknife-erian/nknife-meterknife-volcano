using System;
using System.Collections.Generic;
using NKnife.Channels.Interfaces.Channels;

namespace MeterKnife.Base.Channels
{
    public abstract class MeasureQuestionGroup<T> : List<MeasureQuestion<T>>, IQuestionGroup<T>
    {
        #region Implementation of IEnumerable<out IQuestion<T>>

        /// <summary>����һ��ѭ�����ʼ��ϵ�ö������</summary>
        /// <returns>������ѭ�����ʼ��ϵ� <see cref="T:System.Collections.Generic.IEnumerator`1" />��</returns>
        public IEnumerator<IQuestion<T>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of ICollection<IQuestion<T>>

        /// <summary>��ĳ����ӵ� <see cref="T:System.Collections.Generic.ICollection`1" /> �С�</summary>
        /// <param name="item">Ҫ��ӵ� <see cref="T:System.Collections.Generic.ICollection`1" /> �Ķ���</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> ��ֻ���ġ�
        /// </exception>
        public void Add(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>ȷ�� <see cref="T:System.Collections.Generic.ICollection`1" /> �Ƿ�����ض�ֵ��</summary>
        /// <returns>����� <see cref="T:System.Collections.Generic.ICollection`1" /> ���ҵ� <paramref name="item" />����Ϊ true������Ϊ false��</returns>
        /// <param name="item">Ҫ�� <see cref="T:System.Collections.Generic.ICollection`1" /> �ж�λ�Ķ���</param>
        public bool Contains(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     ���ض��� <see cref="T:System.Array" /> ��������ʼ���� <see cref="T:System.Collections.Generic.ICollection`1" /> ��Ԫ�ظ��Ƶ�һ��
        ///     <see cref="T:System.Array" /> �С�
        /// </summary>
        /// <param name="array">
        ///     ��Ϊ�� <see cref="T:System.Collections.Generic.ICollection`1" /> ���Ƶ�Ԫ�ص�Ŀ��λ�õ�һά
        ///     <see cref="T:System.Array" />��<see cref="T:System.Array" /> ������д��㿪ʼ��������
        /// </param>
        /// <param name="arrayIndex">
        ///     <paramref name="array" /> �д��㿪ʼ�����������ڴ˴���ʼ���ơ�
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array" /> Ϊ null��
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <paramref name="arrayIndex" /> С�� 0��
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="array" /> �Ƕ�ά���顣- �� -Դ <see cref="T:System.Collections.Generic.ICollection`1" /> �е�Ԫ�������ڴ�
        ///     <paramref name="arrayIndex" /> ��Ŀ�� <paramref name="array" /> ��β��֮��Ŀ��ÿռ䡣- �� -�޷��Զ������� <paramref name="T" /> ǿ��ת��ΪĿ��
        ///     <paramref name="array" /> �����͡�
        /// </exception>
        public void CopyTo(IQuestion<T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>�� <see cref="T:System.Collections.Generic.ICollection`1" /> ���Ƴ��ض�����ĵ�һ��ƥ���</summary>
        /// <returns>
        ///     ����Ѵ� <see cref="T:System.Collections.Generic.ICollection`1" /> �гɹ��Ƴ� <paramref name="item" />����Ϊ true������Ϊ
        ///     false�������ԭʼ <see cref="T:System.Collections.Generic.ICollection`1" /> ��û���ҵ� <paramref name="item" />���÷���Ҳ�᷵�� false��
        /// </returns>
        /// <param name="item">Ҫ�� <see cref="T:System.Collections.Generic.ICollection`1" /> ���Ƴ��Ķ���</param>
        /// <exception cref="T:System.NotSupportedException">
        ///     <see cref="T:System.Collections.Generic.ICollection`1" /> ��ֻ���ġ�
        /// </exception>
        public bool Remove(IQuestion<T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>��ȡһ��ֵ����ֵָʾ <see cref="T:System.Collections.Generic.ICollection`1" /> �Ƿ�Ϊֻ����</summary>
        /// <returns>��� <see cref="T:System.Collections.Generic.ICollection`1" /> Ϊֻ������Ϊ true������Ϊ false��</returns>
        public bool IsReadOnly { get; }

        #endregion
    }
}