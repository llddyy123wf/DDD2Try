function bMap(id) {
    this._data = null;

    this.map = null;
    this.initMap(id);
}

bMap.prototype = {
    get_params: function () {
        return mapTools.get_params.call(this.map);
    },
    initMap: function (id, mode) {
        this.map = new BMap.Map(id);
        this.map.centerAndZoom(new BMap.Point(116.404, 39.915), 12);
        this.map.enableScrollWheelZoom(true);
        this.map.addControl(new BMap.NavigationControl());

        var $map = this;
        //拖动事件
        this.map.addEventListener("dragend", function () {
            if ($map._e_dragend) {
                var params = mapTools.get_params.call(this);
                $map._e_dragend(params);
            }
        });
        //缩放事件
        this.map.addEventListener("zoomend", function () {
            if ($map._e_zoomend) {
                var params = mapTools.get_params.call(this);
                $map._e_zoomend(params);
            }
        });
        //点击事件
        this.map.addEventListener("click", function (e) {
            if ($map._e_click) {
                $map._e_click(e.point.lng, e.point.lat);
            }
        });
    },
    set_data: function (data) {
        this._data = data;
        this.addMarker();
    },
    set_cursor: function (cur) {
        switch (cur) {
            case "edit":
                this.map.setDefaultCursor("crosshair");
                break;
            default:
                this.map.setDefaultCursor("default");
                break;
        }


    },
    createIcon: function (icon) {
        var iconImg = new BMap.Icon(icon.img, new BMap.Size(icon.w, icon.h),
                        { imageOffset: new BMap.Size(-icon.l, -icon.t), infoWindowAnchor: new BMap.Size(icon.lb + 5, 1), offset: new BMap.Size(icon.x, icon.h) });
        return iconImg;
    },
    createLabel: function (d) {
        var label = new BMap.Label(d.title, { "offset": new BMap.Size(d.icon.lb - d.icon.x + 10, -20) });
        return label;
    },
    addMarker: function () {
        this.map.clearOverlays();
        for (i = 0, dataLen = this._data.length; i < dataLen; i++) {
            var d = this._data[i];
            //marker
            var point = new BMap.Point(d.point.lng, d.point.lat);
            //var icon = this.createIcon(d.icon);
            var marker = new BMap.Marker(point);
            //var lable = this.createLabel(d);
            //marker.setLabel(lable);
            this.map.addOverlay(marker);
            this.addMarkerHandler(d, marker);
        }
    },
    createEditInfo: function (lng, lat) {
        var ic = "ip地址:" + lng + "," + lat;
        var iw = new BMap.InfoWindow(ic, { enableMessage: false });
        return iw;
    },
    addEditMarker: function (lng, lat) {
        //marker
        var point = new BMap.Point(lng, lat);
        //var icon = this.createIcon(d.icon);
        var marker = new BMap.Marker(point);
        //var lable = this.createLabel(d);
        //marker.setLabel(lable);
        this.map.addOverlay(marker);

        //infow
        var iw = this.createEditInfo(lng, lat);
        marker.openInfoWindow(iw);
    },
    addMarkerHandler: function (d, marker) {
        marker.addEventListener("click", function () {
            var opts = { width: 500, height: 400, enableMessage: false };
            var infoWindow = new BMap.InfoWindow("<div class='thumbnail'><img style='width:100%;' src='" + d.img + "' alt='" + d.title + "'><div class='caption'><h3>" + d.title + "</h3><p>" + d.content + "</p></div></div>", opts);
            this.openInfoWindow(infoWindow);
        }
      );
    },
    bind: function (event, fn) {
        if (event && typeof fn == "function") {
            switch (event) {
                case "dragend":
                    this._e_dragend = fn;
                    break;
                case "zoomend":
                    this._e_zoomend = fn;
                    break;
                case "click":
                    this._e_click = fn;
                    break;
                default:
                    break;
            }
        }
    }
}

mapTools = {
    get_params: function () {
        var bs = this.getBounds();
        //可视区域左下角
        var bssw = bs.getSouthWest();
        //可视区域右上角
        var bsne = bs.getNorthEast();
        //当前地图可视范围是: bssw.lng, bssw.lat, bsne.lng, bsne.lat;

        //获取当前级别
        var level = this.getZoom();
        return { swLng: bssw.lng, swLat: bssw.lat, neLng: bsne.lng, neLat: bsne.lat, level: level };
    }
}