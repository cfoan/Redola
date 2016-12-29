﻿using System;
using Redola.ActorModel;

namespace Redola.Rpc
{
    public class RpcActor
    {
        public static readonly IActorMessageEncoder DefaultActorMessageEncoder = new ActorMessageEncoder(new ProtocolBuffersMessageEncoder());
        public static readonly IActorMessageDecoder DefaultActorMessageDecoder = new ActorMessageDecoder(new ProtocolBuffersMessageDecoder());

        private BlockingRouteActor _localActor = null;

        public RpcActor()
            : this(DefaultActorMessageEncoder, DefaultActorMessageDecoder)
        {
        }

        public RpcActor(IActorMessageEncoder encoder, IActorMessageDecoder decoder)
        {
            if (encoder == null)
                throw new ArgumentNullException("encoder");
            if (decoder == null)
                throw new ArgumentNullException("decoder");

            var configruation = new RpcActorConfiguration();
            configruation.Build();

            _localActor = new BlockingRouteActor(configruation, encoder, decoder);
        }

        public BlockingRouteActor Actor { get { return _localActor; } }
        public IActorMessageEncoder Encoder { get { return _localActor.Encoder; } }
        public IActorMessageDecoder Decoder { get { return _localActor.Decoder; } }

        public void Bootup()
        {
            _localActor.Bootup();
        }

        public void Shutdown()
        {
            _localActor.Shutdown();
        }

        public void RegisterRpcService(RpcService service)
        {
            _localActor.RegisterMessageHandler(service);
        }

        public ActorMessageEnvelope<P> Send<R, P>(string remoteActorType, ActorMessageEnvelope<R> request)
        {
            return Send<R, P>(remoteActorType, request, TimeSpan.FromSeconds(30));
        }

        public ActorMessageEnvelope<P> Send<R, P>(string remoteActorType, ActorMessageEnvelope<R> request, TimeSpan timeout)
        {
            return _localActor.SendMessage<R, P>(remoteActorType, request, timeout);
        }

        public void Send<T>(ActorIdentity remoteActor, ActorMessageEnvelope<T> message)
        {
            _localActor.Send(remoteActor, message);
        }

        public void BeginSend<T>(ActorIdentity remoteActor, ActorMessageEnvelope<T> message)
        {
            _localActor.BeginSend(remoteActor, message);
        }

        public IAsyncResult BeginSend<T>(ActorIdentity remoteActor, ActorMessageEnvelope<T> message, AsyncCallback callback, object state)
        {
            return _localActor.BeginSend(remoteActor, message, callback, state);
        }

        public void EndSend(ActorIdentity remoteActor, IAsyncResult asyncResult)
        {
            _localActor.EndSend(remoteActor, asyncResult);
        }

        public void Send<T>(string remoteActorType, string remoteActorName, ActorMessageEnvelope<T> message)
        {
            _localActor.Send(remoteActorType, remoteActorName, message);
        }

        public void BeginSend<T>(string remoteActorType, string remoteActorName, ActorMessageEnvelope<T> message)
        {
            _localActor.BeginSend(remoteActorType, remoteActorName, message);
        }

        public IAsyncResult BeginSend<T>(string remoteActorType, string remoteActorName, ActorMessageEnvelope<T> message, AsyncCallback callback, object state)
        {
            return _localActor.BeginSend(remoteActorType, remoteActorName, message, callback, state);
        }

        public void Send<T>(string remoteActorType, ActorMessageEnvelope<T> message)
        {
            _localActor.BeginSend(remoteActorType, message);
        }

        public void BeginSend<T>(string remoteActorType, ActorMessageEnvelope<T> message)
        {
            _localActor.BeginSend(remoteActorType, message);
        }

        public void Broadcast<T>(string remoteActorType, ActorMessageEnvelope<T> message)
        {
            _localActor.Broadcast(remoteActorType, message);
        }

        public void BeginBroadcast<T>(string remoteActorType, ActorMessageEnvelope<T> message)
        {
            _localActor.BeginBroadcast(remoteActorType, message);
        }

        public void EndSend(string remoteActorType, string remoteActorName, IAsyncResult asyncResult)
        {
            _localActor.EndSend(remoteActorType, remoteActorName, asyncResult);
        }
    }
}
