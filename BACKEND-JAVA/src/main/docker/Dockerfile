FROM openjdk:8u102-jre
ARG finalName
ADD $finalName /backend-java.jar
VOLUME /tmp
ENTRYPOINT ["java","-jar","/backend-java.jar"]

