// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ProtocolClient.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Protobuf.Protocol {

  /// <summary>Holder for reflection information generated from ProtocolClient.proto</summary>
  public static partial class ProtocolClientReflection {

    #region Descriptor
    /// <summary>File descriptor for ProtocolClient.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ProtocolClientReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRQcm90b2NvbENsaWVudC5wcm90bxIIUHJvdG9jb2wifQoERGF0YRIKCgJp",
            "ZBgBIAEoAxIQCghtYXBMZXZlbBgCIAEoBRIRCgltYXRjaFJvb20YAyABKAUS",
            "IAoGcGxheWVyGAQgAygLMhAuUHJvdG9jb2wuUGxheWVyEiIKB29idGFjbGUY",
            "BSADKAsyES5Qcm90b2NvbC5PYnRhY2xlIkUKB09idGFjbGUSCgoCaWQYASAB",
            "KAMSDQoFc2hhcGUYAiABKAUSCQoBeBgDIAEoAhIJCgF5GAQgASgCEgkKAXoY",
            "BSABKAIiNQoGUGxheWVyEgoKAmlkGAEgASgDEgkKAXgYAiABKAISCQoBeRgD",
            "IAEoAhIJCgF6GAQgASgCKsMBCgZJTkdBTUUSCwoHQ09OTkVDVBAAEgkKBVNU",
            "QVJUEAESCQoFTEVBVkUQAhIPCgtQTEFZRVJfTU9WRRADEhEKDU9CU1RBQ0xF",
            "X01PVkUQBBILCgdOT19NT1ZFEAUSEAoMR0FNRV9DT01QTFRFEAYSDQoJR0FN",
            "RV9GQUlMEAcSDQoJR0FNRV9EUk9QEAgSEAoMUExBWUVSX0NSQVNIEAkSEgoO",
            "T0JTVEFDTEVfQ1JBU0gQChIPCgtQQUNLRVRfRkFJTBALQhuqAhhHb29nbGUu",
            "UHJvdG9idWYuUHJvdG9jb2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::Google.Protobuf.Protocol.INGAME), }, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.Data), global::Google.Protobuf.Protocol.Data.Parser, new[]{ "Id", "MapLevel", "MatchRoom", "Player", "Obtacle" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.Obtacle), global::Google.Protobuf.Protocol.Obtacle.Parser, new[]{ "Id", "Shape", "X", "Y", "Z" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.Protocol.Player), global::Google.Protobuf.Protocol.Player.Parser, new[]{ "Id", "X", "Y", "Z" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Enums
  public enum INGAME {
    [pbr::OriginalName("CONNECT")] Connect = 0,
    [pbr::OriginalName("START")] Start = 1,
    [pbr::OriginalName("LEAVE")] Leave = 2,
    [pbr::OriginalName("PLAYER_MOVE")] PlayerMove = 3,
    [pbr::OriginalName("OBSTACLE_MOVE")] ObstacleMove = 4,
    [pbr::OriginalName("NO_MOVE")] NoMove = 5,
    [pbr::OriginalName("GAME_COMPLTE")] GameComplte = 6,
    [pbr::OriginalName("GAME_FAIL")] GameFail = 7,
    [pbr::OriginalName("GAME_DROP")] GameDrop = 8,
    [pbr::OriginalName("PLAYER_CRASH")] PlayerCrash = 9,
    [pbr::OriginalName("OBSTACLE_CRASH")] ObstacleCrash = 10,
    [pbr::OriginalName("PACKET_FAIL")] PacketFail = 11,
  }

  #endregion

  #region Messages
  public sealed partial class Data : pb::IMessage<Data>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Data> _parser = new pb::MessageParser<Data>(() => new Data());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Data> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.ProtocolClientReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Data() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Data(Data other) : this() {
      id_ = other.id_;
      mapLevel_ = other.mapLevel_;
      matchRoom_ = other.matchRoom_;
      player_ = other.player_.Clone();
      obtacle_ = other.obtacle_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Data Clone() {
      return new Data(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private long id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "mapLevel" field.</summary>
    public const int MapLevelFieldNumber = 2;
    private int mapLevel_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MapLevel {
      get { return mapLevel_; }
      set {
        mapLevel_ = value;
      }
    }

    /// <summary>Field number for the "matchRoom" field.</summary>
    public const int MatchRoomFieldNumber = 3;
    private int matchRoom_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int MatchRoom {
      get { return matchRoom_; }
      set {
        matchRoom_ = value;
      }
    }

    /// <summary>Field number for the "player" field.</summary>
    public const int PlayerFieldNumber = 4;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Protocol.Player> _repeated_player_codec
        = pb::FieldCodec.ForMessage(34, global::Google.Protobuf.Protocol.Player.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Protocol.Player> player_ = new pbc::RepeatedField<global::Google.Protobuf.Protocol.Player>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Google.Protobuf.Protocol.Player> Player {
      get { return player_; }
    }

    /// <summary>Field number for the "obtacle" field.</summary>
    public const int ObtacleFieldNumber = 5;
    private static readonly pb::FieldCodec<global::Google.Protobuf.Protocol.Obtacle> _repeated_obtacle_codec
        = pb::FieldCodec.ForMessage(42, global::Google.Protobuf.Protocol.Obtacle.Parser);
    private readonly pbc::RepeatedField<global::Google.Protobuf.Protocol.Obtacle> obtacle_ = new pbc::RepeatedField<global::Google.Protobuf.Protocol.Obtacle>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<global::Google.Protobuf.Protocol.Obtacle> Obtacle {
      get { return obtacle_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Data);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Data other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (MapLevel != other.MapLevel) return false;
      if (MatchRoom != other.MatchRoom) return false;
      if(!player_.Equals(other.player_)) return false;
      if(!obtacle_.Equals(other.obtacle_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0L) hash ^= Id.GetHashCode();
      if (MapLevel != 0) hash ^= MapLevel.GetHashCode();
      if (MatchRoom != 0) hash ^= MatchRoom.GetHashCode();
      hash ^= player_.GetHashCode();
      hash ^= obtacle_.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (MapLevel != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(MapLevel);
      }
      if (MatchRoom != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(MatchRoom);
      }
      player_.WriteTo(output, _repeated_player_codec);
      obtacle_.WriteTo(output, _repeated_obtacle_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (MapLevel != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(MapLevel);
      }
      if (MatchRoom != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(MatchRoom);
      }
      player_.WriteTo(ref output, _repeated_player_codec);
      obtacle_.WriteTo(ref output, _repeated_obtacle_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
      }
      if (MapLevel != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MapLevel);
      }
      if (MatchRoom != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(MatchRoom);
      }
      size += player_.CalculateSize(_repeated_player_codec);
      size += obtacle_.CalculateSize(_repeated_obtacle_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Data other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0L) {
        Id = other.Id;
      }
      if (other.MapLevel != 0) {
        MapLevel = other.MapLevel;
      }
      if (other.MatchRoom != 0) {
        MatchRoom = other.MatchRoom;
      }
      player_.Add(other.player_);
      obtacle_.Add(other.obtacle_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 16: {
            MapLevel = input.ReadInt32();
            break;
          }
          case 24: {
            MatchRoom = input.ReadInt32();
            break;
          }
          case 34: {
            player_.AddEntriesFrom(input, _repeated_player_codec);
            break;
          }
          case 42: {
            obtacle_.AddEntriesFrom(input, _repeated_obtacle_codec);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 16: {
            MapLevel = input.ReadInt32();
            break;
          }
          case 24: {
            MatchRoom = input.ReadInt32();
            break;
          }
          case 34: {
            player_.AddEntriesFrom(ref input, _repeated_player_codec);
            break;
          }
          case 42: {
            obtacle_.AddEntriesFrom(ref input, _repeated_obtacle_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Obtacle : pb::IMessage<Obtacle>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Obtacle> _parser = new pb::MessageParser<Obtacle>(() => new Obtacle());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Obtacle> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.ProtocolClientReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Obtacle() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Obtacle(Obtacle other) : this() {
      id_ = other.id_;
      shape_ = other.shape_;
      x_ = other.x_;
      y_ = other.y_;
      z_ = other.z_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Obtacle Clone() {
      return new Obtacle(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private long id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "shape" field.</summary>
    public const int ShapeFieldNumber = 2;
    private int shape_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Shape {
      get { return shape_; }
      set {
        shape_ = value;
      }
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 3;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 4;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    /// <summary>Field number for the "z" field.</summary>
    public const int ZFieldNumber = 5;
    private float z_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Obtacle);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Obtacle other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Shape != other.Shape) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Z, other.Z)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0L) hash ^= Id.GetHashCode();
      if (Shape != 0) hash ^= Shape.GetHashCode();
      if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
      if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
      if (Z != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Z);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (Shape != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Shape);
      }
      if (X != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Z);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (Shape != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Shape);
      }
      if (X != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(Z);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
      }
      if (Shape != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Shape);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Obtacle other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0L) {
        Id = other.Id;
      }
      if (other.Shape != 0) {
        Shape = other.Shape;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
      if (other.Z != 0F) {
        Z = other.Z;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 16: {
            Shape = input.ReadInt32();
            break;
          }
          case 29: {
            X = input.ReadFloat();
            break;
          }
          case 37: {
            Y = input.ReadFloat();
            break;
          }
          case 45: {
            Z = input.ReadFloat();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 16: {
            Shape = input.ReadInt32();
            break;
          }
          case 29: {
            X = input.ReadFloat();
            break;
          }
          case 37: {
            Y = input.ReadFloat();
            break;
          }
          case 45: {
            Z = input.ReadFloat();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Player : pb::IMessage<Player>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Player> _parser = new pb::MessageParser<Player>(() => new Player());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Player> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.Protocol.ProtocolClientReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Player() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Player(Player other) : this() {
      id_ = other.id_;
      x_ = other.x_;
      y_ = other.y_;
      z_ = other.z_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Player Clone() {
      return new Player(this);
    }

    /// <summary>Field number for the "id" field.</summary>
    public const int IdFieldNumber = 1;
    private long id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public long Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "x" field.</summary>
    public const int XFieldNumber = 2;
    private float x_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float X {
      get { return x_; }
      set {
        x_ = value;
      }
    }

    /// <summary>Field number for the "y" field.</summary>
    public const int YFieldNumber = 3;
    private float y_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Y {
      get { return y_; }
      set {
        y_ = value;
      }
    }

    /// <summary>Field number for the "z" field.</summary>
    public const int ZFieldNumber = 4;
    private float z_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Z {
      get { return z_; }
      set {
        z_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Player);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Player other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(X, other.X)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Y, other.Y)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Z, other.Z)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0L) hash ^= Id.GetHashCode();
      if (X != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(X);
      if (Y != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Y);
      if (Z != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Z);
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (X != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Z);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Id != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Id);
      }
      if (X != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(X);
      }
      if (Y != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Y);
      }
      if (Z != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Z);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Id);
      }
      if (X != 0F) {
        size += 1 + 4;
      }
      if (Y != 0F) {
        size += 1 + 4;
      }
      if (Z != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Player other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0L) {
        Id = other.Id;
      }
      if (other.X != 0F) {
        X = other.X;
      }
      if (other.Y != 0F) {
        Y = other.Y;
      }
      if (other.Z != 0F) {
        Z = other.Z;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 21: {
            X = input.ReadFloat();
            break;
          }
          case 29: {
            Y = input.ReadFloat();
            break;
          }
          case 37: {
            Z = input.ReadFloat();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Id = input.ReadInt64();
            break;
          }
          case 21: {
            X = input.ReadFloat();
            break;
          }
          case 29: {
            Y = input.ReadFloat();
            break;
          }
          case 37: {
            Z = input.ReadFloat();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
