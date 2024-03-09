/**
 * Created by lulu15 on 17/9/25.
 */
/*!
 * sina.com.cn/license
 * svn:../ui/product/recommender/trunk
 * 20140814111122
 * [${p_id},${t_id},${d_id}] published at ${publishdate} ${publishtime}
 */

(function (con) {
  function dummy() {}
  for (var methods = ['error', 'info', 'log', 'warn', 'clear'], func; func = methods.pop();) {
    con[func] = con[func] || dummy;
  }
}(window.console = window.console || {}));
console = window.console;

/**
 * 跨子域存储，ie6,7使用user data存储，其它浏览器使用localstorage
 * @example
 *      // sina.com.cn域,数据存在news.sina.com.cn下
 *      var Store = window.___CrossDomainStorage___;
 *      Store..ready(function(st){
 *          st.set('key','value');
 *          var data = st.get('key');
 *      });
 *      // 如果用于非sina.com.cn域，需要设置，如
 *      Store.config({
 *          // 设置顶级域
 *          domain:'weibo.com',
 *          // 发布和http://news.sina.com.cn/iframe/87/store.html一样的代理页面，以后数据都存在data.weibo.com下
 *          url:'data.weibo.com/xx/xx/store.html'
 *      }).ready(function(st){
 *          st.set('key','value');
 *          var data = st.get('key');
 *      });
 */
;
(function (exports, name) {
  var fns = [];
  var isReady = 0;
  var iframeStore = null;
  var EXPORTNAME = name || '___CrossDomainStorage___';
  var HANDLE = EXPORTNAME + '.onReady';
  var opt = {
    domain: 'sina.com.cn',
    url: '//news.sina.com.cn/iframe/87/store.html'
  };
  var ERROR = {
    domain: 'fail to set domain!'
  };
  var loadStore = function () {
    if (iframeStore) {
      return;
    }
    try {
      document.domain = opt.domain;
    } catch (e) {
      throw new Error(ERROR.domain);
      return;
    }
    var node = document.getElementById(EXPORTNAME);
    if (node) {
      node.parentNode.removeChild(node);
    }
    var iframeWrap = document.createElement('div');
    var doc = document.body;
    var iframe = '<iframe src="' + opt.url + '?handle=' + HANDLE + '&domain=' + opt.domain + '" frameborder="0"></iframe>';
    var px = '-' + 1e5 + 'em';
    iframeWrap.style.position = 'absolute';
    iframeWrap.style.left = px;
    iframeWrap.style.top = px;
    iframeWrap.className = 'hidden';
    iframeWrap.id = EXPORTNAME;
    iframeWrap.innerHTML = iframe;
    doc.insertBefore(iframeWrap, doc.childNodes[0]);
  };

  var checkReady = function () {
    if (!isReady) {
      loadStore();
    }
    return isReady;
  };
  var CrossDomainStorage = {};
  CrossDomainStorage.ready = function (fn) {
    if (!checkReady()) {
      //ifrmae还没加载
      fns.push(fn);
      return;
    }
    fn(iframeStore);
  };
  CrossDomainStorage.onReady = function (store) {
    if (isReady) {
      return
    }
    isReady = 1;
    iframeStore = store;
    if (fns) {
      while (fns.length) {
        fns.shift()(store);
      }
    }
    fns = null
  };
  CrossDomainStorage.config = function (o) {
    if (!o) {
      return
    }
    for (var i in o) {
      if (o.hasOwnProperty(i)) {
        opt[i] = o[i] || opt[i];
      }
    }
    return this;
  };
  exports[EXPORTNAME] = CrossDomainStorage;
})(window);


;
(function (exports) {
  var Util = {
    byId: function (id) {
      return document.getElementById(id);
    },
    byAttr: function (node, attname, attvalue) {
      var nodes = [];
      attvalue = attvalue || '';
      var getAttr = function (node) {
        return node.getAttribute(attname);
      };
      for (var i = 0, l = node.childNodes.length; i < l; i++) {
        if (node.childNodes[i].nodeType == 1) {
          var fit = false;
          if (attvalue) {
            fit = (getAttr(node.childNodes[i]) == attvalue);
          } else {
            fit = (getAttr(node.childNodes[i]) != '')
          }
          if (fit) {
            nodes.push(node.childNodes[i]);
          }
          if (node.childNodes[i].childNodes.length > 0) {
            nodes = nodes.concat(arguments.callee.call(null, node.childNodes[i], attname, attvalue));
          }
        }
      }
      return nodes;
    },
    bindEvent: function (o, s, fn) {
      if (o.attachEvent) {
        o.attachEvent('on' + s, fn);
      } else {
        o.addEventListener(s, fn, false);
      }
      return o;
    },
    builder: function (wrap, type) {
      var list, nodes;
      nodes = this.byAttr(wrap, type);

      list = {};
      for (var i = 0, len = nodes.length; i < len; i++) {
        var j = nodes[i].getAttribute(type);
        if (!j) {
          continue;
        }
        list[j] || (list[j] = []);
        list[j].push(nodes[i])
      }
      return {
        box: wrap,
        list: list
      }
    },
    strLeft2: (function () {
      var byteLen = function (str) {
        if (typeof str == "undefined") {
          return 0;
        }
        var aMatch = str.match(/[^\x00-\x80]/g);
        return (str.length + (!aMatch ? 0 : aMatch.length));
      };
      return function (str, len) {
        var s = str.replace(/\*/g, " ").replace(/[^\x00-\xff]/g, "**");
        str = str.slice(0, s.slice(0, len).replace(/\*\*/g, " ").replace(/\*/g, "").length);
        if (byteLen(str) > len) str = str.slice(0, str.length - 1);
        return str;
      };
    })(),
    isArray: function (o) {
      return Object.prototype.toString.call(o) === '[object Array]';
    },
    getGuid: function () {
      return Math.abs((new Date()).getTime()) + '_' + Math.round(Math.random() * 1e8);
    },
    extend: function (target, source, deep) {
      target = target || {};
      var sType = typeof source,
        i = 1,
        options;
      if (sType === 'undefined' || sType === 'boolean') {
        deep = sType === 'boolean' ? source : false;
        source = target;
        target = this;
      }
      if (typeof source !== 'object' && Object.prototype.toString.call(source) !== '[object Function]') {
        source = {};
      }
      while (i <= 2) {
        options = i === 1 ? target : source;
        if (options !== null) {
          for (var name in options) {
            var src = target[name],
              copy = options[name];
            if (target === copy) {
              continue;
            }
            if (deep && copy && typeof copy === 'object' && !copy.nodeType) {
              target[name] = this.extend(src || (copy.length !== null ? [] : {}), copy, deep);
            } else if (copy !== undefined) {
              target[name] = copy;
            }
          }
        }
        i++;
      }
      return target;
    },
    cookie: (function () {
      /**
       * 读取cookie,注意cookie名字中不得带奇怪的字符，在正则表达式的所有元字符中，目前 .[]$ 是安全的。
       * @param {Object} cookie的名字
       * @return {String} cookie的值
       * @example
       * var value = co.getCookie(name);
       */
      var co = {};
      co.getCookie = function (name) {
        name = name.replace(/([\.\[\]\$])/g, '\\\$1');
        var rep = new RegExp(name + '=([^;]*)?;', 'i');
        var co = document.cookie + ';';
        var res = co.match(rep);
        if (res) {
          return unescape(res[1]) || "";
        } else {
          return "";
        }
      };

      /**
       * 设置cookie
       * @param {String} name cookie名
       * @param {String} value cookie值
       * @param {Number} expire Cookie有效期，单位：小时
       * @param {String} path 路径
       * @param {String} domain 域
       * @param {Boolean} secure 安全cookie
       * @example
       * co.setCookie('name','sina',null,"")
       */
      co.setCookie = function (name, value, expire, path, domain, secure) {
        var cstr = [];
        cstr.push(name + '=' + escape(value));
        if (expire) {
          var dd = new Date();
          var expires = dd.getTime() + expire * 3600000;
          dd.setTime(expires);
          cstr.push('expires=' + dd.toGMTString());
        }
        if (path) {
          cstr.push('path=' + path);
        }
        if (domain) {
          cstr.push('domain=' + domain);
        }
        if (secure) {
          cstr.push(secure);
        }
        document.cookie = cstr.join(';');
      };

      /**
       * 删除cookie
       * @param {String} name cookie名
       */
      co.deleteCookie = function (name) {
        document.cookie = name + '=;' + 'expires=Fri, 31 Dec 1999 23:59:59 GMT;';
      };
      return co;
    })(),

    // Util.jsonp(url, 'dpc=1', loadedFnName, true);
    jsonp: function (url, params, cb, fix) {
      var head = document.getElementsByTagName('head')[0];
      var idStr = url + '&' + params;
      var ojs = Util.byId(idStr);
      ojs && head.removeChild(ojs);
      var fun = '';
      var js = document.createElement('script');
      fix = fix || false;
      if (fix) {
        if (typeof cb == 'string') {
          fun = cb;
        }
      } else {
        //添加时间戳
        url = url + ((url.indexOf('?') == -1) ? '?' : '&') + '_t=' + Math.random();
        //添加回调
        if (typeof cb == 'function') {
          fun = 'fun_' + Util.getGuid();
          eval(fun + '=function(res){cb(res)}');
        }
      }
      url = url + '&callback=' + fun;
      //添加参数,放在最后，dpc=1一般放在最后
      url = url + '&' + params;
      js.src = url;
      js.id = idStr;
      js.type = 'text/javascript';
      js.language = 'javascript';
      head.appendChild(js);

    },
    jsLoad: function (url, cb) {
      var head = document.getElementsByTagName('head')[0];
      var js = document.createElement('script'),
        isLoaded = false;
      js.onload = js.onreadystatechange = function () {
        if (!isLoaded && (!this.readyState || this.readyState == 'loaded' || this.readyState == 'complete')) {
          isLoaded = true;
          js.onload = js.onreadystatechange = null;
          typeof cb == 'function' && cb();
        }
      };
      js.src = url;
      try {
        head.appendChild(js);
      } catch (e) {}
    },

    uaTrack: function (key, val) {
      if (typeof _S_uaTrack == 'function') {
        try {
          _S_uaTrack(key, val);
        } catch (e) {}
      }
    },
    timeoutHandle: (function () {
      // events = {
      //     'id':{
      //         timer:null,
      //         time:10,
      //         isSuccess:false,
      //         timeout:function(){}
      //     }
      // };
      var events = [];
      var handle = {
        success: function (id) {
          var eve = events[id];
          if (!eve) {
            return;
          }
          eve.isSuccess = true;
          clearTimeout(eve.timer);

        },
        timeout: function (id, fn) {
          var eve = events[id];
          if (!eve) {
            return;
          }
          eve.timer = setTimeout(function () {
            if (eve.isSuccess) {
              return;
            }
            if (typeof fn == 'function') {
              fn.call(this);
            }
          }, eve.time);
        }
      };
      return function (id, fn, time) {
        if (events[id]) {
          throw new Error(id + '瀹歌尙绮＄悮顐㈠窗閻拷');
          return;
        }
        events[id] = {};
        events[id].time = time || 5e3;
        events[id].isSuccess = false;
        if (typeof fn == 'function') {
          fn.call(this, handle);
        }
      }
    })(),
    queryToJson: function (query, isDecode) {
      var qList = query.split("&");
      var json = {};
      for (var i = 0, len = qList.length; i < len; i++) {
        if (qList[i]) {
          hash = qList[i].split("=");
          key = hash[0];
          val = hash[1];
          // 如果只有key没有value,value设置为''
          if (hash.length < 2) {
            val = '';
          }
          // 如果缓存堆栈中没有这个数据
          if (!json[key]) {
            json[key] = val;
          }
        }
      }
      return json;
    }
  };
  Util.app = {

    /**
     * 过滤本页及“已读”数据
     * @param  {Array}   data 需要过滤的数据
     * @param  {Function} fn   过滤完数据后的回调，过滤后的数据为参数
     */
    filter: function (data, fn) {
      var self = this;
      // 过滤数据 会过滤掉本页面及阅读历史
      // 把本页添加到已读数组里，方便统一过滤
      var addItSelf = (function () {
        var url = encodeURIComponent(location.href);
        var oUrl = decodeURIComponent((url.split('?')[0]).split('#')[0]);
        return function (arr) {
          arr.push(oUrl);
          return arr;
        };
      })();

    }
  };
  var Clz = function (parent) {
    var klass = function () {
      this.init.apply(this, arguments);
    };
    if (parent) {
      var subclass = function () {};
      subclass.prototype = parent.prototype;
      klass.prototype = new subclass;
    };
    klass.prototype.init = function () {};
    klass.fn = klass.prototype;
    klass.fn.parent = klass;
    klass._super = klass.__proto__;
    klass.extend = function (obj) {
      var extended = obj.extended;
      for (var i in obj) {
        klass[i] = obj[i];
      }
      if (extended) extended(klass)
    };
    klass.include = function (obj) {
      var included = obj.included;
      for (var i in obj) {
        klass.fn[i] = obj[i];
      }
      if (included) included(klass)
    };
    return klass;
  };
  Util.Clz = Clz;
  /**
   * 兴趣数据加载器
   * @param {String} api 接口地址
   * @param {String} 数据类型 可选 '','video','slide','blog','news',默认为''
   * @param {Function} loadComplete 加载完成后回调，加载到数据为参数
   */
  var Loader = new Clz;
  Loader.include({
    /**
     * 初始化
     */
    init: function (opt) {
      var self = this;
      // 设置状态
      self.setStat();
      // 设置选项
      self.setOpt(opt);
      // 获取数据
      self.getData();
    },
    /**
     * 设置用户自定义选项
     * @param {Object} opt 自定义选项
     */
    setOpt: function (opt) {
      var self = this;
      self.opt = self.opt || {
        // 数据地址
        api: '//interest.mix.sina.com.cn/api/cate/get',
        // 数据类型
        type: '',
        dpc: '',
        // 加载完成后
        loadComplete: function () {},
        // 超时时间
        time: 3e3,
        // 超时处理
        error: function (msg) {}
      };
      var selfOpt = self.opt;
      if (opt || '') {
        selfOpt = Util.extend(selfOpt, opt, true);
      }
    },
    setStat: function () {
      var self = this;
      self._data = null;
    },
    /**
     * 获取guid,如何不存在，则种植该cookie后返回
     * @return {String} guid
     */
    getGuid: function () {
      // 检测cookie是否有guid及guid合法性
      var isVaild = function (guid) {
        guid = parseInt(guid || '0');
        if (guid <= 0) {
          return false;
        }
        return true;
      };
      // 生成新的guid
      var genGuid = function () {
        return Util.getGuid();
      };
      var cookie = Util.cookie;
      var guid = cookie.getCookie('SGUID');
      // 非法guid需要重新生成，并存储到cookie里
      if (!isVaild(guid)) {
        guid = genGuid();
        // 5年
        cookie.setCookie('SGUID', guid, 43800, '/', 'sina.com.cn');
      }
      return guid;
    },
    /**
     * 获取兴趣数据
     */
    getData: function () {
      var self = this;
      var opt = self.opt;

      var loadedFnName = 'cb_' + Util.getGuid();
      var TIMEOUT_NAME = 'loadrecommenderdata' + Util.getGuid();
      var guid = self.getGuid();
      var api = self.opt.api;
      var dpcParam = opt.dpc ? 'dpc=1' : '';
      if (api.indexOf('?') != -1) {
        api = api + '&';
      } else {
        api = api + '?';
      }
      var url = api + 'rnd=' + Util.getGuid();
      Util.timeoutHandle(TIMEOUT_NAME, function (handle) {
        // 回调
        exports[loadedFnName] = function (msg) {
          handle.success(TIMEOUT_NAME);
          self._data = msg;
          // 存在数据
          if (msg.data && msg.data.length > 0) {
            self._loadComplete(msg);
            opt.loadComplete(msg);
          } else {
            opt.error({
              type: 'invaild-data',
              msg: 'invail data'
            });
          }

        };
        // 超时处理
        handle.timeout(TIMEOUT_NAME, function () {
          opt.error({
            type: 'timeout',
            msg: TIMEOUT_NAME + ' ' + opt.time + ' timeout'
          });
        });
        // 请求数据
        Util.jsonp(url, dpcParam, loadedFnName, true);
      }, opt.time);
    },
    _loadComplete: function (m) {}
  });
  /**
   * 获取兴趣数据，后按一定逻辑分页，通过pageComplete回调返回分页后的数据
   * @param {Function} pageComplete 分页后回调，分页数据为参数
   */
  var PageLoader = new Clz(Loader);
  PageLoader.include({
    /**
     * 初始化
     */
    init: function (opt) {
      var self = this;
      // 设置选项
      self.setOpt(opt);
      // 获取数据
      self.getData();
    },
    /**
     * 设置用户自定义选项
     * @param {Object} opt 自定义选项
     */
    setOpt: function (opt) {
      var self = this;
      self.opt = self.opt || {
        // 数据地址
        api: '//interest.mix.sina.com.cn/api/cate/get',
        // 数据类型
        type: '',
        listNum: 10,
        pageNum: 10,
        handleData: function () {},
        // 加载完成后
        loadComplete: function () {},
        // 分页完成后
        pageComplete: function () {},
        // 超时时间
        time: 3e3,
        // 超时处理
        error: function (msg) {}
      };
      var selfOpt = self.opt;
      if (opt || '') {
        selfOpt = Util.extend(selfOpt, opt, true);
      }

    },
    _loadComplete: function (msg) {
      var dataHandled = this.opt.handleData(msg);
      if (typeof dataHandled !== 'undefined' && dataHandled) {
        msg = dataHandled;
      }
      this.page(msg);
    },
    /**
     * 分页
     * @param  {Object} msg 分页前数据
     */
    page: function (msg) {
      var arraySlice = function (data, listNum, pageNum) {
        var len = data.length,
          index = 0,
          to, i, pages = [];
        pageNum = pageNum || Infinity;
        listNum = listNum || len;
        for (i = 0; i < pageNum; i++) {
          to = index + listNum;
          if (to > (len + 3)) {
            break; //修改接口
          }
          pages.push(data.slice(index, to));
          index += listNum;
        }
        return pages;
      };
      var self = this;
      var data = [];
      if (msg) {
        if (msg.data) {
          data = msg.data;
        }
        if (msg.top) {
          data = data.concat(msg.top);
        }
      }
      var pages = arraySlice(data, parseInt(self.opt.listNum, 10), parseInt(self.opt.pageNum, 10));
      self.opt.pageComplete(pages);
    }
  });

  var Recommender = {};
  Recommender.register = function (namespace, method) {
    var i = 0,
      un = Recommender,
      ns = namespace.split('.'),
      len = ns.length,
      upp = len - 1,
      key;
    while (i < len) {
      key = ns[i];
      if (i == upp) {
        if (un[key] !== undefined) {
          throw ns + ':: has registered';
        }
        un[key] = method;
      }
      if (un[key] === undefined) {
        un[key] = {}
      }
      un = un[key];
      i++;
    }
  };
  Recommender.register('util', Util);
  Recommender.register('Clz', Clz);
  Recommender.register('Loader', Loader);
  Recommender.register('PageLoader', PageLoader);
  var EXPORTS_NAME = 'SinaRecommender';
  var UGLIFY_NAME = '___' + EXPORTS_NAME + '___';
  exports[UGLIFY_NAME] = Recommender;
  if (exports[EXPORTS_NAME]) {
    throw '个性化推荐全局变量名"' + EXPORTS_NAME + '"已经被占用，可使用' + UGLIFY_NAME;
  } else {
    exports[EXPORTS_NAME] = Recommender;
  }

})(window);

SHM.register('home.guess.init', function ($) {
  var byId = $.dom.byId;
  var loaded = false;
  var PageLoader = null;
  var Scroller = null;
  var o = {};
  var uaTrack = function (key, val) {
    if (typeof _S_uaTrack == 'function') {
      try {
        _S_uaTrack(key, val);
      } catch (e) {}
    }
  };
  o.init = function () {
    var hasTouch = (typeof (window.ontouchstart) !== 'undefined');
    var viewCollect = (function () {
      var attr = 'page-data';
      var collected = {};
      var inited = false;
      var data = null;
      var collect = function (page) {
        if (collected[page]) {
          return;
        }
        page = parseInt(page, 10);
        var urls = (function () {
          var urls = [];
          var pageData = data[page];
          for (var i = 0, len = pageData.length; i < len; i++) {
            var item = pageData[i];
            urls.push(encodeURIComponent(item.url));
          }
          return urls.join(',');
        })();
        uaTrack('recmd_news_view', urls);
        collected[page] = 1;
      };
      return function (wrap, pages) {
        data = pages;
        if (inited) {
          return;
        }
        $.evt.addEvent(wrap, 'click', function (e) {
          e = e || window.event;
          var target = e.target || e.srcElement;
          var page = target.getAttribute(attr);
          if (page) {
            collect(page);
          }
        });
      };

    })();
    var getGuessData = function () {
      var wrap = byId('SI_Guess_Wrap');
      if (!wrap) {
        return;
      }
      var pageLen = wrap.getAttribute('page-length') || Infinity;
      var listLen = wrap.getAttribute('list-length') || Infinity;
      var itemLen = wrap.getAttribute('item-length') || Infinity;
      var changeBtn = document.getElementById(wrap.getAttribute('change-btn')) || null;
      var byteLen = function (str) {
        var m = str.match(/[^\x00-\x80]/g);
        return (str.length + (!m ? 0 : m.length));
      };
      var strLeft2 = function (str, len) {
        var s = str.replace(/\*/g, " ").replace(/[^\x00-\xff]/g, "**");
        str = str.slice(0, s.slice(0, len).replace(/\*\*/g, " ").replace(/\*/g, "").length);
        if (byteLen(str) > len) str = str.slice(0, str.length - 1);
        return str;
      };

      var cutTitle = function (title, type) {
        if (itemLen == Infinity) {
          return title;
        }
        switch (type) {
          case 'slide':
            title = strLeft2(title, (itemLen - 1) * 2);
            break;
          case 'video':
            title = strLeft2(title, (itemLen - 1) * 2);
            break;
          default:
            title = strLeft2(title, itemLen * 2);
            break;
        }
        return title;
      };

      var bindEvent = function () {
        var pages = wrap.getElementsByTagName('ul');
        var len = pages.length;
        if (len < 2) {
          return;
        }
        // 删除之前初始化添加的事件
        var control = byId('SI_Guess_Control');
        control && (control.style.display = '');
        if (control || Scroller) {
          control.innerHTML = '<a class="mod-guess-prev" href="javascript:;" title="上一帧" id="SI_Guess_Prev" hidefocus="true">上一帧</a> <span class="mod-guess-dots" id="SI_Guess_Dots"> </span> <a class="mod-guess-next" href="javascript:;" title="下一帧" id="SI_Guess_Next" hidefocus="true">下一帧</a>';
        }
        //这里修改
        var $ = jQuery;
        var carousel = {
          init: function (opt) {
            var i = 0;
            var that = this;
            var $Box = opt.BoxId;
            if (typeof ($Box) === 'undefined') {
              return;
            }
            var $leftB = opt.leftBtn;
            var $rightB = opt.rightBtn;
            var time = opt.transitionTime;
            var $pointsBox = opt.pointsBox;

            var autoPlay = true;
            if (typeof (opt.autoPlay) == 'undefined') {
              autoPlay = false;
            } else {
              autoPlay = opt.autoPlay;
            }
            if (opt.itemWidth) {
              var boxW = opt.itemWidth;
            } else {
              var boxW = $Box.css('width').replace('px', '');
            }


            var $lis = $Box.find(".list-a");
            //克隆第一张图片
            var clone = $lis.first().clone();
            //复制到列表最后

            $Box.find(".mod-guess-cont").append(clone);
            var size = $Box.find(".mod-guess-cont .list-a").size();
            $Box.width = size * boxW;
            $Box.find('.mod-guess-cont').width(size * boxW);
            for (var j = 0; j < size - 1; j++) {
              $pointsBox.append("<span class='dotItem' title='第" + (j + 1) + "页'></span>");
            }

            $pointsBox.find(".dotItem").first().addClass("current");
            addEvents();

            function addEvents() {
              /*自动轮播*/
              if (autoPlay) {
                var t = setInterval(function () {
                  i++;
                  move();
                }, time || 3000);
              }

              /*鼠标悬停事件*/

              $Box.hover(function () {

                clearInterval(t); //鼠标悬停时清除定时器
              }, function () {
                if (autoPlay) {
                  t = setInterval(function () {
                    i++;
                    move();
                  }, time || 3000); //鼠标移出时清除定时器
                }
              });

              /*鼠标滑入原点事件*/
              $pointsBox.find(".dotItem").hover(function () {
                // 如果是克隆的图片，不让他执行

                var _left = Math.abs($Box.find(".mod-guess-cont").css('left').replace('px', ''));
                // console.log(_left)

                var index = $(this).index(); //获取当前索引值
                // console.log(index)
                if (_left == (size - 1) * boxW && index == 0) {
                  return;
                }
                i = index;
                // i++;
                move();
                // $Box.find(".img").stop().animate({ left: -index * boxW }, 500);
                $(this).addClass("current").siblings().removeClass("current");
              }, function () {
                var _left = Math.abs($Box.find(".mod-guess-cont").css('left').replace('px', ''));
                // console.log(_left)

                var index = $(this).index(); //获取当前索引值
                // console.log(index)
                if (_left == (size - 1) * boxW && index == 0) {
                  $Box.find(".mod-guess-cont").stop().css({
                    left: 0
                  });

                }
              });

              /*向左按钮*/
              if (typeof ($leftB) !== 'undefined') {
                $leftB.on('click', function () {
                  i--;
                  move();
                })
              }

              /*向右按钮*/
              if (typeof ($rightB) !== 'undefined') {
                $rightB.on('click', function () {
                  i++;
                  move();
                })
              }
            }

            function move() {
              if (i == size) {
                $Box.find(".mod-guess-cont").css({
                  left: 0
                });
                i = 1;
              }
              if (i == -1) {
                $Box.find(".mod-guess-cont").css({
                  left: -(size - 1) * boxW
                });
                i = size - 2;
              }
              $Box.find(".mod-guess-cont").stop().animate({
                left: -i * boxW
              }, 500);

              if (i == size - 1) {
                $pointsBox.find(".dotItem").eq(0).addClass("current").siblings().removeClass("current");
              } else {
                $pointsBox.find(".dotItem").eq(i).addClass("current").siblings().removeClass("current");
                $pointsBox.find(".dotItem").eq(i).addClass("current").siblings().removeClass("current");
              }
            }

          }
        }
        var guess_config = {
          BoxId: $('#SI_Order_Guess'),
          leftBtn: $('#SI_Order_Guess .mod-guess-prev'),
          rightBtn: $('#SI_Order_Guess .mod-guess-next'),
          transitionTime: 15000,
          pointsBox: $('#SI_Order_Guess .mod-guess-dots'),
          autoPlay: true
        }
        carousel.init(guess_config);
        //修改结束

        byId('SI_Guess_Prev').onmousedown = function () {
          uaTrack('index_new_guess', 'change');
        }
        byId('SI_Guess_Next').onmousedown = function () {
          uaTrack('index_new_guess', 'change');
        }
      };
      var html = '';
      var pageComplete = function (data) {
        var pages = data,
          list, item, title,
          html = '';
        var typeClz = {
          '3': 'videoNewsLeft',
          '2': 'slideNewsLeft'
        };
        var getType = function (t) {
          var type = typeClz[t];
          return typeof type == 'undefined' ? '' : type;
        };
        var guess = function (item, i, linkProStr) {
          title = item.stitle ? item.stitle.replace(/<\/?[^>]*>/g, '') : item.title.replace(/<\/?[^>]*>/g, '');
          var urls_param = item.url + "?cre=sinapc&mod=g";
          var url = item.url;
          if (url.indexOf('slide') > 0) {
            var url1 = url.replace(/(http:\/\/)|(https:\/\/)/, '');
            url = window.url + url1;
          }
          var s = url.substring(url.length - 4);
          var src = item.thumb;
          var viewCollectStr = dataVersion == 'c' ? ' page-data="' + i + '" ' : '';
          if (item.thumb) {
            if (s == "html") {
              html += '<a ' + viewCollectStr + ' class="guess guess_pic ' + getType(item.type) + '" ' + linkProStr + '"href="' + urls_param + '" title="' + title + '">' + '<img src="' + src + '" alt=""><span>' + title + '</span></a>';
            } else {
              if (i == 0) {
                html += '<a ' + viewCollectStr + ' class="guess guess_pic ' + getType(item.type) + '" ' + linkProStr + '"href="' + url + '" title="' + title + '"style="margin-right:14px;"><img src="' + src + '"><span>' + title + '</span></a>';
              } else {
                html += '<a ' + viewCollectStr + ' class="guess guess_pic ' + getType(item.type) + '" ' + linkProStr + '"href="' + url + '" title="' + title + '"><img src="' + src + '"><span>' + title + '</span></a>';
              }
            }
          }
        }
        var linkProStr = ' target="_blank" suda-uatrack="key=index_new_guess&value=' + dataVersion + '_click';

        var length = pages.length < pageLen ? pages.length : pageLen;
        for (var i = 0; i < length; i++) {
          list = pages[i];
          var len = list.length;
          var viewCollectStr = dataVersion == 'c' ? ' page-data="' + i + '" ' : '';
          if (i != length - 1 && len < listLen) {
            // 修改接口
            break;
          }
          if (i < length - 1) {
            html += '<ul class="list-a slide-a list-a-201406121603">';
            for (var j = 0; j < len; j++) {
              item = list[j];
              //过滤html标签，如 <font color="#ff0000">亚冠-8024爆射穆里奇单刀斩</font>
              title = item.title.replace(/<\/?[^>]*>/g, '');
              if (title.length > 25) {
                title = item.stitle ? item.stitle.replace(/<\/?[^>]*>/g, '') : title;
              }
              var url = item.url_https ? item.url_https : item.url;
              var urls_param = url + "?cre=sinapc&mod=g";
              var s = url.substring(url.length - 4);
              if (s == "html") {
                html += '<li>' + '<a ' + viewCollectStr + ' class="' + getType(item.type) + '" ' + linkProStr + '" href="' + urls_param + '" title="' + title + '">' + cutTitle(title, item.type) + '</a></li>';
              } else {
                html += '<li>' + '<a ' + viewCollectStr + ' class="' + getType(item.type) + '" ' + linkProStr + '" href="' + url + '" title="' + title + '">' + cutTitle(title, item.type) + '</a></li>';
              }
            }
            html += '</ul>';
          } 
          // else {
          //   var len1 = 2;
          //   html += '<ul class="list-a slide-a list-a-201406121603">';
          //   if (SHM.dom.byClass('guess_pic').length == 0) {
          //     html += '<div class="guess_slide_scroll">';
          //     if (!window.pic1.pic) {
          //       guess(list[0], 0, linkProStr);
          //     } else {
          //       html += '<a  class="guess guess_pic" href="' + window.pic1.url + '"' + linkProStr + 'title="' + window.pic1.title + '"style="margin-right:14px;"><img src="' + window.pic1.pic + '"><span>' + window.pic1.title + '</span></a>';
          //     }
          //     if (!window.pic2.pic) {
          //       guess(list[1], 1, linkProStr);
          //     } else {
          //       html += '<a  class="guess guess_pic" href="' + window.pic2.url + '"' + linkProStr + 'title="' + window.pic2.title + '"><img src="' + window.pic2.pic + '"><span>' + window.pic2.title + '</span></a>';
          //     }
          //     html += '</div>';

          //   }

          //   for (var j = 2; j < 5; j++) {
          //     item = list[j];
          //     title = item.title.replace(/<\/?[^>]*>/g, '');
          //     if (title.length > 25) {
          //       title = item.stitle ? item.stitle.replace(/<\/?[^>]*>/g, '') : title;
          //     }
          //     var urls_param = item.url + "?cre=sinapc&mod=g";
          //     var url = item.url;
          //     var s = url.substring(url.length - 4);
          //     if (s == "html") {
          //       html += '<li>' + '<a ' + viewCollectStr + ' class="' + getType(item.type) + '" ' + linkProStr + '"href="' + urls_param + '" title="' + title + '">' + cutTitle(title, item.type) + '</a></li>';
          //     } else {
          //       html += '<li>' + '<a ' + viewCollectStr + ' class="' + getType(item.type) + '" ' + linkProStr + '"href="' + item.url + '" title="' + title + '">' + cutTitle(title, item.type) + '</a></li>';
          //     }
          //   }
          //   html += '</ul>';
          // }
        }
        wrap.innerHTML = html;
        bindEvent();
        loaded = true;
        SHMUATrack('guess', dataVersion + '_pageview');
        viewCollect(wrap, data);
      };
      var SinaRecommender = window.___SinaRecommender___;
      var Util = SinaRecommender.util;

      var dataVersion = 'c';
      PageLoader = new SinaRecommender.PageLoader({
        api: "//cre.mix.sina.com.cn/api/v3/get?cateid=sina_all&cre=tianyi&mod=pchp&merge=3&statics=1&length=" + listLen * pageLen + "&up=0&down=0&fields=url_https,media,labels_show,title,url,info,thumbs,mthumbs,thumb,ctime,reason,vtype,category&tm=1514342107&action=0&offset=0&top_id=", //修改接口
        listNum: listLen,
        pageNum: pageLen,
        // suda 统计5秒算超时
        time: 5e3,
        pageComplete: pageComplete,
        error: function (error) {
          Loader = PageLoader;
          if (error.type == 'timeout' || error.type == 'invaild-data') {
            if (error.type == 'timeout') {
              SHMUATrack('guess', dataVersion + '_timeout');
            }
            var getTDate = function () {
              var curDate = new Date(),
                year = curDate.getFullYear(),
                month = curDate.getMonth() + 1,
                day = curDate.getDate();
              if (month < 10) {
                month = '0' + month;
              }
              if (day < 10) {
                day = '0' + day;
              }
              return year + '' + month + day;
            };

            var url = '//top.news.sina.com.cn/ws/GetTopDataList.php?top_type=day&top_cat=www_www_all_suda_12_suda&top_time=' + getTDate() + '&top_show_num=' + (Loader.opt.listNum * Loader.opt.pageNum) + '&top_order=ASC&format=json&__app_key=ls';

            var loadedFnName = 'cb_' + Util.getGuid();
            // 回调
            window[loadedFnName] = function (msg) {
              dataVersion = 'c_hotlist';
              msg = msg.result;
              Loader._data = msg;
              Loader._loadComplete(msg);
              Loader.opt.loadComplete(msg);
            };
            // 请求排行数据
            Util.jsonp(url, 'dpc=1', loadedFnName, true);
          }
        }
      });

    };
    getGuessData();

    var loginBtn = $.E('SI_Guess_Login_Btn');
    if (!loginBtn) {
      return;
    }
    var loginLayer = window.SINA_OUTLOGIN_LAYER;
    var custAdd = $.evt.custEvent.add;
    var custRemove = $.evt.custEvent.remove;
    //直接拿cookie判断预防loginSuccess失效
    var cookie = sinaSSOController.get51UCCookie();
    if (cookie && loginBtn) {
      loginBtn.style.display = 'none';
    }
    custAdd($, 'loginSuccess', function () {
      if (loginBtn) {
        loginBtn.style.display = 'none';
      }
    });
    custAdd($, 'logoutSuccess', function () {
      if (loginBtn) {
        loginBtn.style.display = 'none';
      }
    });
    $.evt.addEvent(loginBtn, 'click', function () {
      // suda 猜你喜欢 登录
      SHMUATrack('guess', 'weibo_hotshow_signin');
      // if(!loginLayer.isLogin()){
      // loginLayer.setMode('simple');
      // loginLayer.set('plugin', {
      //     mode : 'simple'
      // })
      loginLayer.set('plugin', {
          position: 'top,right',
          parentNode: null,
          relatedNode: loginBtn
        })
        .set('drag', {
          enable: true,
          dragarea: 'page'
        })
        .set('styles', {
          'marginLeft': 0,
          'marginTop': '40px'
        })
        .show();
      custAdd($, 'loginSuccess', function () {
        if (PageLoader) {
          PageLoader.getData();
        } else {
          getGuessData();
        }
      });
    });
  };
  o.isLoaded = function () {
    return loaded;
  };
  o.init();
  return o;
});
SHM.register('home.guess.isLoaded', function ($) {
  return function () {
    return $.home.guess.init.isLoaded();
  };
});