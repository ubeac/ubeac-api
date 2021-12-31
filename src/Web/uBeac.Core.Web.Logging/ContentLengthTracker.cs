namespace uBeac.Web.Logging
{
    public class ContentLengthTracker
    {
        public long ContentLength { get; set; } = 0;
    }

    public class ContentLengthTrackingStream : Stream
    {
        private readonly Stream _inner;
        private readonly ContentLengthTracker _tracker;

        public ContentLengthTrackingStream(Stream inner, ContentLengthTracker tracker)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _tracker = tracker ?? throw new ArgumentNullException(nameof(tracker));
        }

        public override bool CanRead => _inner.CanRead;

        public override bool CanSeek => _inner.CanSeek;

        public override bool CanWrite => _inner.CanWrite;

        public override long Length => _inner.Length;

        public override long Position
        {
            get => _inner.Position;
            set => _inner.Position = value;
        }

        public override bool CanTimeout => _inner.CanTimeout;

        public override int ReadTimeout
        {
            get => _inner.ReadTimeout;
            set => _inner.ReadTimeout = value;
        }

        public override int WriteTimeout
        {
            get => _inner.WriteTimeout;
            set => _inner.WriteTimeout = value;
        }

        public ContentLengthTracker Tracker => _tracker;

        public override void Flush() => _inner.Flush();

        public override Task FlushAsync(CancellationToken cancellationToken)
            => _inner.FlushAsync(cancellationToken);

        public override int Read(byte[] buffer, int offset, int count)
            => _inner.Read(buffer, offset, count);

        public async override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
            => await _inner.ReadAsync(buffer.AsMemory(offset, count), cancellationToken);

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            => _inner.BeginRead(buffer, offset, count, callback, state);

        public override int EndRead(IAsyncResult asyncResult)
        {
            if (asyncResult is Task<int> task)
            {
                return task.GetAwaiter().GetResult();
            }

            return _inner.EndRead(asyncResult);
        }

        public override long Seek(long offset, SeekOrigin origin)
            => _inner.Seek(offset, origin);

        public override void SetLength(long value)
           => _inner.SetLength(value);

        public override void Write(byte[] buffer, int offset, int count)
        {
            _tracker.ContentLength += count - offset;
            _inner.Write(buffer, offset, count);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            _tracker.ContentLength += count - offset;
            return _inner.BeginWrite(buffer, offset, count, callback, state);
        }

        public override void EndWrite(IAsyncResult asyncResult)
            => _inner.EndWrite(asyncResult);

        public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            _tracker.ContentLength += count - offset;
            return _inner.WriteAsync(buffer, offset, count, cancellationToken);
        }

        public override void WriteByte(byte value)
        {
            _tracker.ContentLength++;
            _inner.WriteByte(value);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _inner.Dispose();
            }
        }
    }

}
