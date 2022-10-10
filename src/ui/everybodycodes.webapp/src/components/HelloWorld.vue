<template>
<div>
  <h1>Security cameras Utrecht</h1>
  <div id="map">
 <GmapMap
                      :center="map_center"
                      :zoom="2"
                      map-type-id="terrain"
                      style="width: 800px; height: 400px"
                    >
                      <gmap-info-window
                        :options="{
                          maxWidth: 300,
                          pixelOffset: { width: 0, height: -35 },
                        }"
                        :position="infoWindow.position"
                        :opened="infoWindow.open"
                        @closeclick="infoWindow.open = false"
                      >
                        <div v-html="infoWindow.template"></div>
                      </gmap-info-window>
                      <GmapMarker
                         v-for="camera in cameras" 
                         :key="camera.code"
                        :position="GetPosition(camera)"
                        :clickable="true"
                        :draggable="false"
                        @click="openInfoWindowTemplate(camera)"
                      />
                    </GmapMap>

  </div>
  <div id="source">
    source:
    <a href="https://data.overheid.nl/dataset/camera-s">https://data.overheid.nl/dataset/camera-s</a>


  </div>

    <div id="source">

    <input v-model="searchKeyword" placeholder="search cameras" />
    <button @click="Search()">Search</button>
  </div>
  <main>
    <table id="cameraTableContainer">
      <tbody>
        <tr>
          <td>
            <table id="column3">
              <thead>
                <tr>
                  <th colspan="4">Cameras 3</th>
                </tr>
                   <tr>
                  <th>Number</th>
                  <th>Name</th>
                  <th>Latitude</th>
                  <th>Longitude</th>
                </tr>
                <tr v-for="camera in firstCameras" :key="camera.code" >
                   <th>{{camera.code}}</th>
                  <th>{{camera.name}}</th>
                  <th>{{camera.latitude}}</th>
                  <th>{{camera.longitude}}</th>
                </tr>

              </thead>
              <tbody>
              </tbody>
            </table>
          </td>
          <td>
            <table id="column5">
              <thead>
                <tr>
                  <th colspan="4">Cameras 5</th>
                </tr>
                <tr>
                  <th>Number</th>
                  <th>Name</th>
                  <th>Latitude</th>
                  <th>Longitude</th>
                </tr>
                        <tr v-for="camera in secondCameras" :key="camera.code" >
                   <th>{{camera.code}}</th>
                  <th>{{camera.name}}</th>
                  <th>{{camera.latitude}}</th>
                  <th>{{camera.longitude}}</th>
                </tr>
              </thead>
              <tbody>
              </tbody>
            </table>
          </td>
          <td>
            <table id="column15">
              <thead>
                <tr>
                  <th colspan="4">Cameras 3 &amp; 5</th>
                </tr>
                <tr>
                  <th>Number</th>
                  <th>Name</th>
                  <th>Latitude</th>
                  <th>Longitude</th>
                </tr>
                        <tr v-for="camera in thirdCameras" :key="camera.code" >
                   <th>{{camera.code}}</th>
                  <th>{{camera.name}}</th>
                  <th>{{camera.latitude}}</th>
                  <th>{{camera.longitude}}</th>
                </tr>
              </thead>
              <tbody>
              </tbody>
            </table>
          </td>
          <td>
            <table id="columnOther">
              <thead>
                <tr>
                  <th colspan="4">Cameras Overig</th>
                </tr>
                <tr>
                  <th>Number</th>
                  <th>Name</th>
                  <th>Latitude</th>
                  <th>Longitude</th>
                </tr>
                        <tr v-for="camera in fourthCameras" :key="camera.code" >
                   <th>{{camera.code}}</th>
                  <th>{{camera.name}}</th>
                  <th>{{camera.latitude}}</th>
                  <th>{{camera.longitude}}</th>
                </tr>
              </thead>
              <tbody>
              </tbody>
            </table>
          </td>
        </tr>
      </tbody>
    </table>
  </main>
  </div>

</template>

<script>
import axios from 'axios'

export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      cameras: [],
      firstCameras: [],
      secondCameras: [],
      thirdCameras: [],
      fourthCameras: [],
      map_center: { lat: 52.0914, lng: 5.1115 },
      infoWindow: {
        position: { lat: 0, lng: 0 },
        open: false,
        template: "",
      },
      searchKeyword: ""
    };
  },
  mounted () {
    console.log("mount started")
    
    axios
      .get('https://localhost:7027/api/camera')
      .then(response => {
        
        this.cameras = response.data
        console.log(this.cameras);
        this.firstCameras = this.cameras.filter(x=>x.code % 3 == 0);
        this.secondCameras = this.cameras.filter(x=>x.code % 5 == 0);
        this.thirdCameras = this.cameras.filter(x=>x.code % 3 == 0 && x.code % 5 == 0);
        this.fourthCameras = this.cameras.filter(x=>x.code % 3 != 0 && x.code % 5 != 1);
        })
  },
  methods : {
      GetPosition(marker) {

      return {
        lat: parseFloat(marker.latitude),
        lng: parseFloat(marker.longitude),
      };
    },
    ChangeCenter(camera) {
      this.map_center.lat = camera.latitude;
      this.map_center.lng = camera.longitude;
    },
    openInfoWindowTemplate(camera) {
      this.map_center.lat = camera.latitude;
      this.map_center.lng = camera.longitude;
      this.infoWindow.position.lat = camera.latitude;
      this.infoWindow.position.lng = camera.longitude;
      this.infoWindow.template = `<br><b>${camera.name}</b><br>`;
      this.infoWindow.open = true;
    },
    Search()
    {
      console.log(this.searchKeyword);

       axios
      .get('https://localhost:7027/api/camera/' + this.searchKeyword)
      .then(response => {
        console.log(response)
        this.cameras = response.data
        console.log(this.cameras);
        this.firstCameras = this.cameras.filter(x=>x.code % 3 == 0);
        this.secondCameras = this.cameras.filter(x=>x.code % 5 == 0);
        this.thirdCameras = this.cameras.filter(x=>x.code % 3 == 0 && x.code % 5 == 0);
        this.fourthCameras = this.cameras.filter(x=>x.code % 3 != 0 && x.code % 5 != 1);
        })
  },
    
  }
}


// import apiConnector from "../../api/apiconnector";
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
